function displayMsg() {
    const compiler = (document.getElementById("compiler") as HTMLInputElement).value;
    const framework = (document.getElementById("framework") as HTMLInputElement).value;
    return `Hey Hey Hey ${compiler} and ${framework}`;
}