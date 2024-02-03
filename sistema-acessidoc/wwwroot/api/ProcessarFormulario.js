document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('upload-form');
    if (form) {
        form.addEventListener('submit', function (event) {
            event.preventDefault();

            const formData = new FormData(form);
            fetch('/upload', {
                method: 'POST',
                body: formData,
            })
                .then(response => {
                    console.log('Resposta recebida:', response);
                    return response.text(); // ou response.json() dependendo do que você espera
                })
                .then(data => {
                    console.log('Dados da resposta:', data);
                })
                .catch(error => {
                    console.error('Erro na solicitação POST:', error);
                });


        });
    }
});
