$(document).ready(function () {
  loadHtml("layout/nav.html", "nav", (elem) => {
    let navTitle = elem.getElementsByTagName("h1").item(0);
    if (document.title == "Kezdőlap") {
      addSidewayMove();
    }
    navTitle.classList.add("title");
    navTitle.textContent = document.title;
  });

  loadHtml("layout/footer.html", "footer", () => {});
});


