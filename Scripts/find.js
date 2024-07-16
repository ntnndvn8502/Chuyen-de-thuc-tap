function Find() {
    const a = document.getElementById('find-text');
    const b = document.getElementById('find-button');
    b.setAttribute("href", "/Home/Find/" + a.value);
}