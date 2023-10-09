let loginForm = document.getElementById("form");
let currentNumbers = []
loginForm.addEventListener("submit", (e) => {
    e.preventDefault();
    let number = document.getElementById("num");
    console.log(number.value.isNaN)
    if (isNaN(number.value)) {
        return;
    }

    if (parseInt(number.value) <= Math.max(currentNumbers)) {
        return;
    }

    if (currentNumbers.length + 1 >= 5) {
        let btn = document.getElementById("btn");
        btn.disabled = true;
    }

    let list = document.getElementById("list")
    let listElem = document.createElement("li");
    listElem.addEventListener("click", (e) => {
        console.log("elem clicked");
        for (let elem of list.children) {
            if (elem === listElem) {
                elem.style.color = "red";
            } else {
                elem.style.color = "black";
            }
        }
    });

    listElem.append(number.value);
    list.append(listElem);
    currentNumbers.push(parseFloat(number.value));
    for (let elem of list.children) {
        elem.style.color = "black";
    }
    let avg = document.getElementById("avg");
    avg.textContent = currentNumbers.reduce((a, b) => a + b) / currentNumbers.length;

});