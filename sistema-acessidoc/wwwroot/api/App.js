const fs = require('fs');
const path = require('path');
const express = require('express');
const multer = require('multer');
const { PDFDocument, rgb } = require('pdf-lib');
const pdfParse = require('pdf-parse');
const fontkit = require('@pdf-lib/fontkit');

const app = express();
const port = 3000;
const fontBytes = fs.readFileSync('NotoSansSymbols.ttf');

const uploadDir = path.join(__dirname, 'uploads');
const processedDir = path.join(__dirname, 'processed');

if (!fs.existsSync(uploadDir)) {
    fs.mkdirSync(uploadDir, { recursive: true });
}

if (!fs.existsSync(processedDir)) {
    fs.mkdirSync(processedDir, { recursive: true });
}

const ABNT_MARGINS = { top: 3 * 28.35, left: 3 * 28.35, right: 2 * 28.35, bottom: 2 * 28.35 };
const ABNT_LINE_SPACING = 1.5;
const ABNT_PARAGRAPH_INDENT = 1.25 * 28.35;
const FONT_SIZE = { standard: 18, enlarged: 24 };

app.use(express.static('/Views/Login/'));
app.use(express.json());
app.use(express.urlencoded({ extended: true }));

const storage = multer.diskStorage({
    destination: function (req, file, cb) {
        cb(null, 'uploads');
    },
    filename: function (req, file, cb) {
        console.log("Nome do arquivo original:", file.originalname);
        console.log("Extensão do arquivo:", path.extname(file.originalname));
        cb(null, file.fieldname + '-' + Date.now() + path.extname(file.originalname));
    }
});

const fileFilter = (req, file, cb) => {
    if (file.mimetype === 'application/pdf' || file.mimetype === 'application/msword') {
        cb(null, true);
    } else {
        cb(null, false);
        req.fileValidationError = "Permitido apenas PDF e DOC!";
    }
};

const upload = multer({ storage: storage, fileFilter: fileFilter });

app.post('/upload', upload.single('document'), async (req, res) => {
    if (req.fileValidationError) {
        return res.status(400).send(req.fileValidationError);
    }

    const filePath = path.join(uploadDir, req.file.filename);
    const desiredFontSize = req.body.fontSize === 'standard' ? FONT_SIZE.standard : FONT_SIZE.enlarged;

    try {
        const existingPdfBytes = fs.readFileSync(filePath);
        const data = await pdfParse(existingPdfBytes);
        const text = data.text;

        const pdfDoc = await PDFDocument.create();
        pdfDoc.registerFontkit(fontkit);
        const page = pdfDoc.addPage();
        const font = await pdfDoc.embedFont(fontBytes);

        page.drawText(text, {
            x: ABNT_MARGINS.left + ABNT_PARAGRAPH_INDENT,
            y: page.getHeight() - ABNT_MARGINS.top - desiredFontSize,
            size: desiredFontSize,
            font: font,
            color: rgb(0, 0, 0),
            lineHeight: desiredFontSize * ABNT_LINE_SPACING,
            maxWidth: page.getWidth() - ABNT_MARGINS.left - ABNT_MARGINS.right
        });

        const newPdfBytes = await pdfDoc.save();
        const outputFilePath = path.join(processedDir, req.file.filename);
        fs.writeFileSync(outputFilePath, newPdfBytes);

        // Envia o arquivo processado para o servidor ASP.NET Core
        const formData = new FormData();
        formData.append('document', fs.createReadStream(outputFilePath));
        formData.append('fontSize', req.body.fontSize);

        const response = await axios.post('http://localhost:5000/Prova/ProcessarFormulario', formData, {
            headers: {
                ...formData.getHeaders(),
            },
        });

        res.status(response.status).send(response.data);
    } catch (error) {
        console.error("Erro ao processar o PDF:", error);
        res.status(500).send('Erro ao processar o arquivo.');
    }
});

app.listen(port, () => {
    console.log(`Servidor rodando na porta ${port}`);
});