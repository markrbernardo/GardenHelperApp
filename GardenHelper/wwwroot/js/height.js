window.setTwoColHeight = () => {
    const summary = document.getElementById("summaryCard");
    const wrapper = document.getElementById("twoColWrapper");

    if (!summary || !wrapper) return;

    const summaryHeight = summary.offsetHeight;
    const viewportHeight = window.innerHeight;

    wrapper.style.height = (viewportHeight - summaryHeight - 40) + "px";
};

window.addEventListener("resize", () => {
    window.setTwoColHeight();
});
