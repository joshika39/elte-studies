$(document).ready(function () {
  $.get(
    "layout/nav.html",
    function (html_string) {
      var doc = new DOMParser().parseFromString(html_string, "text/html");
      let nav = doc.getElementsByTagName("nav").item(0);
      //   console.log(nav);

      let navTitle = nav.getElementsByTagName("h1").item(0);
      navTitle.classList.add("title");
      //   console.log(navTitle);
      navTitle.textContent = document.title;
      console.log(nav.innerHTML);

      document.getElementById("nav").innerHTML = nav.innerHTML;
    },
    "html"
  );
});

const slider = document.querySelector('.events');
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

slider.addEventListener('mousemove', (e) => {
  e.preventDefault();
  if(!mouseDown) { return; }
  const x = e.pageX - slider.offsetLeft;
  const scroll = x - startX;
  slider.scrollLeft = scrollLeft - scroll;
});

// Add the event listeners
slider.addEventListener('mousedown', startDragging, false);
slider.addEventListener('mouseup', stopDragging, false);
slider.addEventListener('mouseleave', stopDragging, false);