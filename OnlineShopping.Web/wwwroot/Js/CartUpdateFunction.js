function MakeUpdateVisible(id, visible) {
    const updateQtyBtn = document.querySelector("button[data-itemId='" + id + "']");
    console.log("here");
    if (visible == true) {
        updateQtyBtn.style.display = "inline-block";
    }
    else {
        updateQtyBtn.style.display = "none";
    }
}