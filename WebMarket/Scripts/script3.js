function main() {
    let a = document.querySelector("input[type='submit']");
   
    let b = document.getElementById("pass");
    a.addEventListener("click", () => {
        
        b.value = generateHashPass(b.value);
    });

}
main();