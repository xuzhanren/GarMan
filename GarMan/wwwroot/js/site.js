// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Theme Toggle Functionality
function toggleTheme() {
    const html = document.documentElement;
    const currentTheme = html.getAttribute('data-bs-theme');
    const newTheme = currentTheme === 'dark' ? 'light' : 'dark';
    
    html.setAttribute('data-bs-theme', newTheme);
    localStorage.setItem('theme', newTheme);
    
    // Update button icon and text
    updateThemeButton();
}

function updateThemeButton() {
    const themeBtn = document.getElementById('themeToggleBtn');
    if (!themeBtn) return;
    
    const currentTheme = document.documentElement.getAttribute('data-bs-theme');
    const icon = themeBtn.querySelector('i');
    const text = themeBtn.querySelector('.theme-text');
    
    if (currentTheme === 'dark') {
        icon.className = 'bi bi-sun-fill';
        if (text) text.textContent = 'Light Mode';
    } else {
        icon.className = 'bi bi-moon-stars-fill';
        if (text) text.textContent = 'Dark Mode';
    }
}

// Initialize theme button on page load
document.addEventListener('DOMContentLoaded', function() {
    updateThemeButton();
});
