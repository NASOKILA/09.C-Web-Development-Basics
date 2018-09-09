

let closure = (function(){
    let counter = 0;

    return function(){
        counter++;
        return counter;
    }
}());



console.log(closure());
console.log(closure());
console.log(closure());

//1 2 3
