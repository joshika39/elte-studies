/**
 * Helper function to optimize the performance of the scroll event
 * @param func - The function to be throttled
 * @param limit - The time limit to throttle the function
 * @return {(function(): void)|*}
 */
export const throttle = (func, limit) => {
  let inThrottle;
  return function() {
    const args = arguments;
    const context = this;
    if (!inThrottle) {
      func.apply(context, args);
      inThrottle = true;
      setTimeout(() => (inThrottle = false), limit);
    }
  };
}


/**
 * Check if an element is in the viewport
 * @param el - The element to check
 * @param partiallyInView - Whether the element is partially in view
 * @return {boolean}
 */
const isElementInViewport = (el, partiallyInView = false) => {
  const rect = el.getBoundingClientRect();
  let top = rect.top;
  let bottom = rect.bottom;
  let height = rect.height;

  if (partiallyInView) {
    return top + height >= 0 && height + window.innerHeight >= bottom;
  } else {
    return top >= 0 && bottom <= window.innerHeight;
  }
}

export { isElementInViewport };