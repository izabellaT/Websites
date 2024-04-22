var topButton = document.getElementById("topBtn");

window.onscroll = function() {
  scrollFunction();
};

function scrollFunction() {
  if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
    topButton.style.display = "block";
  } else {
    topButton.style.display = "none";
  }
}

topButton.addEventListener("click", function() {
  document.body.scrollTop = 0;
  document.documentElement.scrollTop = 0; 
});



document.addEventListener("DOMContentLoaded", function() {
    var shopDropdown = document.getElementById('shopDropdown');
    var dropdownContent = shopDropdown.querySelector('.dropdown-content');
    
    shopDropdown.addEventListener('mouseenter', function() {
        dropdownContent.style.display = 'block';
    });

    shopDropdown.addEventListener('mouseleave', function() {
        dropdownContent.style.display = 'none';
    });

    var categories = document.querySelectorAll('.category');
    categories.forEach(function(category) {
        category.addEventListener('click', function() {
            var subcontents = this.nextElementSibling;

            document.querySelectorAll('.dropdown-content').forEach(function(subcontent) {
                subcontent.style.display = 'none';
            });

            subcontents.style.display = 'block';
        });
    });
});


document.addEventListener("DOMContentLoaded", function() {
    var slides = document.querySelectorAll('.slideshow img');
    var index = 0;

    function showSlide() {
        slides.forEach(function(slide) {
            slide.style.display = 'none';
        });
        index = (index + 1) % slides.length;
        slides[index].style.display = 'block';
    }

    showSlide(); 

    setInterval(showSlide, 2000); 
});


var slideIndex = 0;
  showSlides();

  function showSlides() {
    var slides = document.getElementsByClassName("teamsMySlides");
    var texts = document.getElementsByClassName("teamsText");
    
    for (var i = 0; i < slides.length; i++) {
      slides[i].style.display = "none";  
      texts[i].style.display = "none";  
    }

    slideIndex++;
    if (slideIndex > slides.length) {slideIndex = 1}    
    slides[slideIndex-1].style.display = "block";
    texts[slideIndex-1].style.display = "block";  

    setTimeout(showSlides, 4000); 
  }