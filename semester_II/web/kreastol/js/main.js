$(function () {

  loadHtml('layout/nav.html', 'navbar', () => {

    const navbarToggle = document.querySelector("#navbar-toggle");
    const navbarMenu = document.querySelector("#navbar-menu");
    const navbarLinksContainer = navbarMenu.querySelector(".navbar-links");
    let isNavbarExpanded = navbarToggle.getAttribute("aria-expanded") === "true";

    function toggleNavbarVisibility() {
      isNavbarExpanded = !isNavbarExpanded;
      navbarToggle.setAttribute("aria-expanded", isNavbarExpanded);
    }

    navbarToggle.addEventListener("click", toggleNavbarVisibility);
    console.log(navbarToggle);

    navbarLinksContainer.addEventListener("click", (e) => e.stopPropagation());
    navbarMenu.addEventListener("click", toggleNavbarVisibility);

    let navTitle = document.querySelector("#navbar .title");
    if (document.title == "Kezdőlap") {
      addSidewayMove();
    }
    navTitle.classList.add("title");
    navTitle.textContent = document.title;

  });

  loadHtml('layout/footer.html', 'footer', () => { });

});

