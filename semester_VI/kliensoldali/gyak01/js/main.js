import {isElementInViewport, throttle} from "./lib.js";

document.addEventListener('DOMContentLoaded', function () {
  // Task 2
  const navbar = document.getElementById('mainNav');
  const scrollThreshold = 200;

  const checkScroll = () => {
      navbar.classList.toggle('navbar-scrolled', window.scrollY > scrollThreshold);
  }
  window.addEventListener('scroll', throttle(checkScroll, 100));

  // Task 3
  const observerCallback = (entries, observer) => {
    entries.forEach(entry => {
      if (entry.isIntersecting) {
        const animation_name = entry.target.getAttribute('data-scroll-animation');
        entry.target.classList.add(`animate__${animation_name}`);
        entry.target.classList.add('animate__animated');
        observer.unobserve(entry.target);
      }
    });
  }
  const elements = document.querySelectorAll('[data-scroll]');
  const task3ObserverOptions = {
    threshold: 0.1
  }
  const task3Observer = new IntersectionObserver(observerCallback, task3ObserverOptions);
  elements.forEach(element => task3Observer.observe(element));

  // Task 4
  const progressBar = document.getElementById('progress-bar');
  function updateProgressBar() {
    const scrollTop = document.documentElement.scrollTop || document.body.scrollTop;
    const scrollHeight = document.documentElement.scrollHeight - document.documentElement.clientHeight;
    const scrollPercentage = (scrollTop / scrollHeight) * 100;
    progressBar.style.width = scrollPercentage + '%';
  }

  window.addEventListener('scroll', throttle(updateProgressBar, 100));

  // Task 5
  const elementsTask5 = document.querySelectorAll('.page-section');
  const navLinks = document.querySelectorAll('.nav-link');
  const task5ObserverOptions = {
    threshold: 0.5
  }
  const task5Observer = new IntersectionObserver((entries, observer) => {
    entries.forEach(entry => {
      const sectionId = entry.target.id;
      const navLink = document.querySelector(`.nav-link[href="#${sectionId}"]`);
      navLink.classList.toggle('active', entry.isIntersecting);
    });
  }, task5ObserverOptions);
  elementsTask5.forEach(element => task5Observer.observe(element));
});
