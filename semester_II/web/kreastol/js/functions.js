function loadHtml(html_file, targetId, modifyElementFunc, parseType = "text/html") {
	$.get(html_file, function (html_string) {
		var elementDom = new DOMParser()
			.parseFromString(html_string, parseType);

		if (elementDom.getElementById(targetId) != null) {

			let element = elementDom.getElementById(targetId);

			let target = document.getElementById(targetId);
			if (target != null) {
				target.innerHTML = element.innerHTML;
			}

			modifyElementFunc(elementDom);
		}
	}, "html");
}

function createNavbar() {
	const navbarToggle = document.querySelector("#navbar-toggle");
	const navbarMenu = document.querySelector("#navbar-menu");
	const navbarLinksContainer = navbarMenu.querySelector(".navbar-links");
	let isNavbarExpanded = navbarToggle.getAttribute("aria-expanded") === "true";

	function toggleNavbarVisibility() {
		isNavbarExpanded = !isNavbarExpanded;
		navbarToggle.setAttribute("aria-expanded", isNavbarExpanded);
	}

	navbarToggle.addEventListener("click", toggleNavbarVisibility);

	navbarLinksContainer.addEventListener("click", (e) => e.stopPropagation());
	navbarMenu.addEventListener("click", toggleNavbarVisibility);

	let navTitle = document.querySelector("#navbar .title");

	navTitle.classList.add("title");
	navTitle.textContent = document.title;
	setCurrentPage(document.title);
}

function setCurrentPage(title) {
	if (title == "Kezdőlap") {
		addSidewayMove();
		if (document.querySelector("#home-page")) {
			document.querySelector("#home-page").style.textDecoration = "underline";
		}
	}
	else if (title == "Hírek") {
		if (document.querySelector("#news-page")) {
			document.querySelector("#news-page").style.textDecoration = "underline";
		}
	}
	else if (title == "Rólunk") {
		if (document.querySelector("#about-page")) {
			document.querySelector("#about-page").style.textDecoration = "underline";
		}
	}
	else if (title == "Kapcsolat") {
		if (document.querySelector("#contact-page")) {
			document.querySelector("#contact-page").style.textDecoration = "underline";
		}
	}
	else if (title == "Profilom") {
		if (document.querySelector("#profile-page")) {
			document.querySelector("#profile-page").style.textDecoration = "underline";
		}
	}
}

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

function getProperty(element, propertyName) {
	var r = document.querySelector(element);
	var rs = getComputedStyle(r);
	return rs.getPropertyValue(propertyName);
}

function setProperty(element, propertyName, newValue) {
	var r = document.querySelector(element);
	r.style.setProperty(propertyName, newValue);
}


function setCookie(cname, cvalue, exdays) {
	const d = new Date();
	d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
	let expires = "expires=" + d.toUTCString();
	document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function getCookie(cname) {
	let name = cname + "=";
	let decodedCookie = decodeURIComponent(document.cookie);
	let ca = decodedCookie.split(';');
	for (let i = 0; i < ca.length; i++) {
		let c = ca[i];
		while (c.charAt(0) == ' ') {
			c = c.substring(1);
		}
		if (c.indexOf(name) == 0) {
			return c.substring(name.length, c.length);
		}
	}
	return false;
}


function largerFont(){
	var size = parseFloat(getProperty(':root', '--font-value'));
	if(size < 1.3){
		size += 0.1;
		setProperty(':root', '--font-value', size + 'rem');
		setCookie('font-size', size + 'rem', 60);
	}

}

function smallerFont(){
	var size = parseFloat(getProperty(':root', '--font-value'));
	if(size > 0.6){
		size -= 0.1;
		setProperty(':root', '--font-value', size + 'rem');
		setCookie('font-size', size + 'rem', 60);
	}
}

function resetFont(){
	setProperty(':root', '--font-value', '.9rem');
	setCookie('font-size', '.9rem', 60);
}

function changeContrast(isHighcontrast){
	setCookie('high-contrast', isHighcontrast, 60);
	if(isHighcontrast){
		setProperty(':root', '--primary-1', "#000");
		setProperty(':root', '--primary-2', "#000");
		setProperty(':root', '--primary-3', "#000");
		setProperty(':root', '--primary-4', "#000");
		setProperty(':root', '--primary', "yellow");
		setProperty(':root', '--primary-6', "#000");
		setProperty(':root', '--primary-7', "#000");
		setProperty(':root', '--primary-8', "#000");
		setProperty(':root', '--primary-9', "#000");
		setProperty(':root', '--accent-1', "yellow");
		setProperty(':root', '--accent-1-2', "orange");
		setProperty(':root', '--accent-2', "black");
		setProperty(':root', '--accent-2-2', "lightblue");
		setProperty(':root', '--on-light-bg', "yellow");
	}
	else{
		setProperty(':root', '--primary-1', "");
		setProperty(':root', '--primary-2', "");
		setProperty(':root', '--primary-3', "");
		setProperty(':root', '--primary-4', "");
		setProperty(':root', '--primary', "");
		setProperty(':root', '--primary-6', "");
		setProperty(':root', '--primary-7', "");
		setProperty(':root', '--primary-8', "");
		setProperty(':root', '--primary-9', "");
		setProperty(':root', '--accent-1', "");
		setProperty(':root', '--accent-1-2', "");
		setProperty(':root', '--accent-2', "");
		setProperty(':root', '--accent-2-2', "");
		setProperty(':root', '--on-light-bg', "");
	}
}