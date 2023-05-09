$(document).ready(function () {
  loadHtml("layout/nav.html", "navbar", (elem) => {

    const navbarToggle = document.querySelector("#navbar-toggle");
    const navbarMenu = document.querySelector("#navbar-menu");
    const navbarLinksContainer = navbarMenu.querySelector(".navbar-links");
    let isNavbarExpanded = navbarToggle.getAttribute("aria-expanded") === "true";

    function toggleNavbarVisibility() {
      isNavbarExpanded = !isNavbarExpanded;
      navbarToggle.setAttribute("aria-expanded", isNavbarExpanded);
    }
    asdasd 
    navbarToggle.addEventListener("click", toggleNavbarVisibility);
    console.log(navbarToggle);

    navbarLinksContainer.addEventListener("click", (e) => e.stopPropagation());
    navbarMenu.addEventListener("click", toggleNavbarVisibility);

    let navTitle = document.querySelector("#navbar .title");
    navTitle.classList.add("title");
    navTitle.textContent = document.title;
  }, "application/xhtml+xml");

  loadHtml("layout/footer.html", "footer", () => {}, "application/xhtml+xml"); 
});