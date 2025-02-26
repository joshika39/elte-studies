class Element extends HTMLElement {
  constructor() {
    super();

    this.attachShadow({ mode: 'open' });
    this.className = this.constructor.name;
  }

  connectedCallback() {
    console.log("Element connected");
    this.loadCss(`components/${this.className}.css`);
  }

  loadCss(file) {
    const link = document.createElement('link');
    link.rel = 'stylesheet';
    link.href = file;
    console.log(link);
    this.shadowRoot.appendChild(link);
  }
}

export { Element };
