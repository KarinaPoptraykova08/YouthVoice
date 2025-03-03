document.addEventListener("DOMContentLoaded", () => {
    // Navbar scroll effect
    const navbar = document.querySelector(".navbar")

    window.addEventListener("scroll", () => {
        if (window.scrollY > 50) {
            navbar.style.padding = "0.5rem 0"
            navbar.style.backgroundColor = "rgba(255, 255, 255, 0.98)"
            navbar.style.boxShadow = "0 2px 10px rgba(0, 0, 0, 0.1)"
        } else {
            navbar.style.padding = "1rem 0"
            navbar.style.backgroundColor = "rgba(255, 255, 255, 0.95)"
            navbar.style.boxShadow = "0 2px 10px rgba(0, 0, 0, 0.1)"
        }
    })

    // Contact form submission
    const contactForm = document.getElementById("contactForm")
    if (contactForm) {
        contactForm.addEventListener("submit", (e) => {
            e.preventDefault()

            const email = document.getElementById("email").value
            const subject = document.getElementById("subject").value
            const message = document.getElementById("message").value

            // Here you would typically send the form data to your server
            console.log("Form submitted:", { email, subject, message })

            // Show success message (in a real app, do this after successful AJAX)
            alert("Thank you for your message! We will get back to you soon.")
            contactForm.reset()
        })
    }

    // Initialize Bootstrap tooltips
    const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
    if (tooltipTriggerList.length > 0) {
        tooltipTriggerList.map((tooltipTriggerEl) => new bootstrap.Tooltip(tooltipTriggerEl))
    }

    // Smooth scrolling for anchor links
    document.querySelectorAll('a[href^="#"]').forEach((anchor) => {
        anchor.addEventListener("click", function (e) {
            e.preventDefault()

            const targetId = this.getAttribute("href")
            if (targetId === "#") return

            const targetElement = document.querySelector(targetId)
            if (targetElement) {
                window.scrollTo({
                    top: targetElement.offsetTop - 70, // Adjust for navbar height
                    behavior: "smooth",
                })
            }
        })
    })
})

