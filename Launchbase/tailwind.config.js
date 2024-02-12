/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ['./**/*.{razor, html}'],
    darkMode: false,
    theme: {
        extend: {
            colors: {
                pr: "#1b1e29",
                sr: "#272b37",
                tr: "#1DAFFD",
                hr: "#272b37",
                nr: "#6DC4E3",
                th: "#313644",
                rahmen: "#41454f",
                tabelle: "#313644",
                tabellenline: "#242834",
                tabellenlinerahmen: "#171a24",
                tabellenzeilen: "#242834",
                rot: "#ff4343"
            }
        },
    },
    variants: {
        extend: {},
    },
    plugins: [],
}
