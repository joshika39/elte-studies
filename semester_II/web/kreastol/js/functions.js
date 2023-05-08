function loadHtml(html_file, targetId, modifyElementFunc, parseType = "text/html") {
	$.get(html_file, function (html_string) {
		var elementDom = new DOMParser()
			.parseFromString(html_string, parseType);
		console.log(document);
		console.log(elementDom.getElementById(targetId));

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