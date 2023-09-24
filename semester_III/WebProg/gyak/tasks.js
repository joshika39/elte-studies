console.log('Hello World');

console.log(percent(80, 10));

function percent(number: number, percent: number): number {
    return number * (percent / 100);
}

function farToCel(temp: number): number {
    return 5 / 9 * (temp - 32);
}

function lnko(a, b) {
    if (a < b) {
        let temp = a;
        a = b;
        b = a;
    }
    let rem = a % b;
    while (rem > 0) {
        a = b;
        b = rem;
        rem = a % b;
    }

    return b;
}

function lkkt(a, b) {
    let x = a;
    let y = b;
    while (x !== y){
        if(x < y) {
            x += a;
        }

        if(x > y) {
            y += b;
        }
    }
    return x;
}

function countNegative() {

}