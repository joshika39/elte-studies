import {Element} from './base/Element.js';

class Carousel extends Element {
  constructor() {
    super();
  }

  connectedCallback() {
    super.connectedCallback();
    this.items = this.querySelectorAll('li');
    this.index = 0;
    this.shadowRoot.appendChild(this.createButton('Prev', this.prev));
    this.shadowRoot.appendChild(this.createButton('Next', this.next));
    this.list = document.createElement('ul');
    this.shadowRoot.appendChild(this.list);
    this.render();
  }

  render() {
    this.list.innerHTML = '';
    this.list.appendChild(this.items[this.index]);
  }

  createButton(text, handler) {
    const button = document.createElement('button');
    button.textContent = text;
    button.addEventListener('click', handler);
    return button;
  }

  next = () => {
    this.index = (this.index + 1) % this.items.length;
    this.render();
  }

  prev = () => {
    this.index = (this.index - 1 + this.items.length) % this.items.length;
    this.render();
  }
}

customElements.define('carousel-element', Carousel);

export default Carousel;



