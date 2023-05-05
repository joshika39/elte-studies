$(document).ready(function () {
  $.get(
    "layout/nav.html",
    function (html_string) {
      var navdoc = new DOMParser().parseFromString(
        html_string,
        "application/xhtml+xml"
      );

      let nav = navdoc.getElementsByTagName("nav").item(0);
      let navTitle = nav.getElementsByTagName("h1").item(0);
      if (document.title == "Kezdőlap") {
        addSidewayMove();
      }
      navTitle.classList.add("title");
      navTitle.textContent = document.title;

      document.getElementById("nav").innerHTML = nav.innerHTML;
    },
    "html"
  );
});

function addSidewayMove() {
  const slider = document.querySelector(".events");
  let mouseDown = false;
  let startX, scrollLeft;

  let startDragging = function (e) {
    mouseDown = true;
    startX = e.pageX - slider.offsetLeft;
    scrollLeft = slider.scrollLeft;
  };
  let stopDragging = function (event) {
    mouseDown = false;
  };

  slider.addEventListener("mousemove", (e) => {
    e.preventDefault();
    if (!mouseDown) {
      return;
    }
    const x = e.pageX - slider.offsetLeft;
    const scroll = x - startX;
    slider.scrollLeft = scrollLeft - scroll;
  });

  slider.addEventListener("mousedown", startDragging, false);
  slider.addEventListener("mouseup", stopDragging, false);
  slider.addEventListener("mouseleave", stopDragging, false);
}
