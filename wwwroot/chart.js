window.setup = (id,myConfig) => {

    var ctx = document.getElementById(id).getContext('2d');
    const labels = myConfig.labels;
    const data = {
    labels: labels,
    datasets: [{
        label: 'Usuários, vagas e projetos',
        data: myConfig.data,
        backgroundColor: myConfig.bg,
        borderWidth: 1
    }]
    };
    const config = {
        type: 'bar',
        data: data,
        options: {
          scales: {
            y: {
              beginAtZero: true
            }
          }
        },
      };
    const a = new Chart(ctx, config);
}
