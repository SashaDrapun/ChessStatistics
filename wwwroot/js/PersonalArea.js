
    let ctx = document.getElementById('ratingChart').getContext('2d');
    let ratingChart = new Chart(ctx, {
        type: 'bar',
    data: {
        labels: ['Блиц', 'Рапид', 'Классика'],
    datasets: [{
        label: 'Рейтинг',
    data: [
    @(Model.Player.RatingBlitz.ToString()),
    @(Model.Player.RatingRapid.ToString()),
    @(Model.Player.RatingClassic.ToString())
    ],
    backgroundColor: [
    'rgba(255, 99, 132, 0.5)',
    'rgba(54, 162, 235, 0.5)',
    'rgba(75, 192, 192, 0.5)' // Изменен цвет классики
    ],
    borderColor: [
    'rgba(255, 99, 132, 1)',
    'rgba(54, 162, 235, 1)',
    'rgba(75, 192, 192, 1)' // Изменен цвет классики
    ],
    borderWidth: 1
                    }]
                },
    options: {
        scales: {
        y: {
        beginAtZero: true,
    max: 3000
                        }
                    },
                }
            });

    var ctx2 = document.getElementById('ratingDistributionChart').getContext('2d');
    var ratingDistributionChart = new Chart(ctx2, {
        type: 'pie',
    data: {
        labels: ['Блиц', 'Рапид', 'Классика'],
    datasets: [{
        label: 'Рейтинг',
    data: [
    @(Model.Player.RatingBlitz.ToString()),
    @(Model.Player.RatingRapid.ToString()),
    @(Model.Player.RatingClassic.ToString())
    ],
    backgroundColor: [
    'rgba(255, 99, 132, 0.5)',
    'rgba(54, 162, 235, 0.5)',
    'rgba(75, 192, 192, 0.5)' // Изменен цвет классики
    ],
    borderColor: [
    'rgba(255, 99, 132, 1)',
    'rgba(54, 162, 235, 1)',
    'rgba(75, 192, 192, 1)' // Изменен цвет классики
    ],
    borderWidth: 1
                    }]
                },
    options: { } // Добавлена пустая секция options
            });