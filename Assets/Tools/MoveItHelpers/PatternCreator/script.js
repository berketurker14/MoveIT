const gridContainer = document.getElementById("gridContainer");
const output = document.getElementById("output");

function generateGrid() {
    // Clear the existing grid
    gridContainer.innerHTML = "";

    const gridSize = parseInt(document.getElementById("gridSize").value);
    const originX = Math.floor(gridSize / 2);
    const originY = Math.floor(gridSize / 2);

    // Update the grid container's styles
    gridContainer.style.gridTemplateRows = `repeat(${gridSize}, 1fr)`;
    gridContainer.style.gridTemplateColumns = `repeat(${gridSize}, 1fr)`;

    for (let i = 0; i < gridSize; i++) {
        for (let j = 0; j < gridSize; j++) {
            const gridItem = document.createElement("div");
            gridItem.classList.add("grid-item");

            if (i === originY && j === originX) {
                gridItem.classList.add("origin");
                gridItem.textContent = "O";
            }

            gridItem.setAttribute("data-x", j);
            gridItem.setAttribute("data-y", i);
            gridItem.addEventListener("click", () => {
                if (!gridItem.classList.contains("origin")) {
                    gridItem.classList.toggle("selected");
                }
            });

            gridContainer.appendChild(gridItem);
        }
    }
}


// Generate the initial grid with the default size
generateGrid();

// Rest of the existing code (generateOffsets, copyToClipboard, toggleTheme)



function generateOffsets() {
    const selectedItems = document.querySelectorAll(".grid-item.selected");
    const offsets = [];

    selectedItems.forEach(item => {
        const x = parseInt(item.getAttribute("data-x")) - 4;
        const y = 4 - parseInt(item.getAttribute("data-y"));
        offsets.push(`new Vector2Int(${x}, ${y})`);
    });

    output.textContent = `List<Vector2Int> offsets = new List<Vector2Int> { ${offsets.join(", ")} };`;
}

function copyToClipboard() {
    const outputText = output.textContent;
    const textArea = document.createElement("textarea");
    textArea.value = outputText;
    document.body.appendChild(textArea);
    textArea.select();
    document.execCommand("copy");
    document.body.removeChild(textArea);

    // Optionally, provide feedback to the user that the text has been copied
    const copyButton = document.getElementById("copyButton");
    copyButton.textContent = "Copied!";
    setTimeout(() => {
        copyButton.textContent = "Copy";
    }, 2000);
}

function toggleTheme() {
    document.body.classList.toggle("dark");
}
