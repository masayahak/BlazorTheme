window.setTheme = (theme) => {
    document.documentElement.setAttribute("data-theme", theme);

    const tables = document.querySelectorAll(".table");
    tables.forEach((table) => {
        if (theme === "dark") {
            table.classList.add("table-dark");
        } else {
            table.classList.remove("table-dark");
        }
    });
};
