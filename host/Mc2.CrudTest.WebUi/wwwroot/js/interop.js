function createAlert() {
    alert("Hey this is an alert froj js");
}

function createQuestion(question) {
    return prompt(question);
}

function setElementHtmlById(id, text) {
    let element = document.getElementById(id);
    if (element !== undefined && element !== null) {
        document.getElementById(id).innerHTML = text;
    }
}

function refreshPage() {
    window.location.reload();
}

function setElementTextById(id, text) {
    let element = document.getElementById(id);
    if (element !== undefined && element !== null) {
        document.getElementById(id).innerText = text;
    }
}

function focusOnElement(element) {
    element.focus();
}

function generateRandomNumberFromJ(maxIntSize) {
    console.log('here i am');
    DotNet.invokeMethodAsync('BlazorServer', 'GenerateRandomNumber', maxIntSize).then(res => {
        setElementTextBytId('radnomNumberSpan', res);
    })
}

function giveMeRandomInt(maxIntSize, dotnetInstace) {
    console.log('giveMeRandomInt');
    dotnetInstace.invokeMethodAsync('MethodFromInstantiateClass', maxIntSize).then(res => {
        setElementTextBytId('radnomNumberSpan', res);
    });
}