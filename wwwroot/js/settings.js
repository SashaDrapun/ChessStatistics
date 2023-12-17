document.querySelector('.languageSelect').addEventListener('change', e => {
    const value = document.querySelector('#languageValue').value;
    console.log(value);
    document.querySelector('#languageValue').value = value == "ru" ? "en" : "ru";
    console.log(document.querySelector('#languageValue').value);
});

document.querySelector('.themeSelect').addEventListener('change', e => {
    const value = document.querySelector('#ThemeValue').value;
    document.querySelector('#ThemeValue').value = (value == "dark" ? "light" : "dark");
});