$(document).ready(function () {
	$.get("layout/nav.html", function (html_string) {

		var doc = new DOMParser().parseFromString(html_string, "text/html");
		let nav = doc.getElementsByTagName("nav").item(0);
		console.log(nav);

		let navTitle = nav.getElementsByTagName("h1").item(0);
		console.log(navTitle);
		navTitle.textContent = document.title;
		console.log(nav.innerHTML);

		document.getElementById("nav").innerHTML = nav.innerHTML;
	}, 'html');


});