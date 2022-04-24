function changeColor(select) {
    var style = select.options[select.selectedIndex].style;
    select.style.color = style.color;
}

function getCookie(name) {
    var match = document.cookie.match(new RegExp('(^| )' + name + '=([^;]+)'));
    if (match) return match[2];
}

function sortTableBy(name) {
    if (getCookie("sortExpensesBy") === name) {
        document.cookie = "isReverseExpenses=" + (getCookie("isReverseExpenses") !== "true");
    }
    else {
        document.cookie = "isReverseExpenses=false"
    }
    document.cookie = "sortExpensesBy=" + name;

    document.location.reload()
}