$(document).ready(function () {
  if (!getCookie('font-size')) {
    setCookie('font-size', getProperty(':root', '--font-value'), 90);
  } else {
    setProperty(':root', '--font-value', getCookie('font-size'));
  }
  
  changeContrast(getCookie('high-contrast') == 'true');

  loadHtml("layout/nav.html", "navbar", createNavbar, "application/xhtml+xml");
  loadHtml("layout/xfooter.html", "footer", () => {}, "application/xhtml+xml"); 
});