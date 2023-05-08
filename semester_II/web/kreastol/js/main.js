
let isNavbarExpanded = false;

$(function () {
  loadHtml("layout/nav.html", "navbar", (elemdom) => {
    // const navbarToggle = elemdom.querySelector("#navbar-toggle");
    // const navbarMenu = elemdom.querySelector("#navbar-menu");
    // const navbarLinksContainer = navbarMenu.querySelector(".navbar-links");
    // // let isNavbarExpanded = navbarToggle.getAttribute("aria-expanded") === "true";
    
    // function toggleNavbarVisibility() {
    //   console.log("toggleNavbarVisibility");
    //   isNavbarExpanded = !isNavbarExpanded;
    //   navbarToggle.setAttribute("aria-expanded", isNavbarExpanded);
    // }
    
    // navbarToggle.addEventListener("click", toggleNavbarVisibility);
    // console.log(navbarToggle);
    
    // navbarLinksContainer.addEventListener("click", (e) => e.stopPropagation());
    // navbarMenu.addEventListener("click", toggleNavbarVisibility);
  }, "text/html");
  const navbarToggle = document.querySelector("#navbar-toggle");
  const navbarMenu = document.querySelector("#navbar-menu");
  const navbarLinksContainer = navbarMenu.querySelector(".navbar-links");
  // let isNavbarExpanded = navbarToggle.getAttribute("aria-expanded") === "true";
  
  function toggleNavbarVisibility() {
    console.log("toggleNavbarVisibility");
    isNavbarExpanded = !isNavbarExpanded;
    navbarToggle.setAttribute("aria-expanded", isNavbarExpanded);
  }
  
  navbarToggle.addEventListener("click", toggleNavbarVisibility);
  console.log(navbarToggle);
  
  navbarLinksContainer.addEventListener("click", (e) => e.stopPropagation());
  navbarMenu.addEventListener("click", toggleNavbarVisibility);
  // loadHtml("layout/footer.html", "footer", () => { });
});




// let navTitle = elem.getElementsByTagName("h1").item(0);
// if (document.title == "Kezdőlap") {
//   addSidewayMove();
// }
// navTitle.classList.add("title");
// navTitle.textContent = document.title;

