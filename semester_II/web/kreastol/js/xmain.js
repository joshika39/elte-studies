$(document).ready(function () {
  loadHtml("layout/nav.html", "nav", (elem) => {
    let navTitle = elem.getElementsByTagName("h1").item(0);
    navTitle.classList.add("title");
    navTitle.textContent = document.title;
  }, "application/xhtml+xml");

  loadHtml("layout/footer.html", "footer", () => {}, "application/xhtml+xml"); 
});