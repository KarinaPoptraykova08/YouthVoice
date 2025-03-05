document.getElementById("cancelBtn").addEventListener("click", function () {
    // Show confirmation alert
    let confirmAction = confirm("Сигурни ли сте че искате да отмените тази организация?");

    // If user clicks "OK", reset the form
    const form = document.getElementById('orgForm');
    if (confirmAction) {        
        form.reset();        
    }

    location.reload(true);
});

document.getElementById("submitBtn").addEventListener("click", function () {
    const form = document.GetElementById('orgForm');

    if (form.checkValidity()) {
        form.reset();
    }

    location.reload(true);
});

function updateCounter() {
    let textarea = document.getElementById("orgDescription");
    let count = textarea.value.length;
    document.getElementById("charCount").textContent = count;
}