(function () {
    'use strict';

    const ANIMAL_TYPES = ['Elephant', 'Giraffe', 'Monkey'];

    // TODO replace with the zoo fetched from the API
    const SAMPLE_ZOO = {
        time: '09:00:00',
        animals: [
            { type: 'Elephant', status: 'Alive', healthPercent: 100 },
            { type: 'Elephant', status: 'CannotWalk', healthPercent: 65.5 },
            { type: 'Giraffe', status: 'Alive', healthPercent: 82.25 },
            { type: 'Monkey', status: 'Dead', healthPercent: 28 }
        ]
    };

    const timeElement = document.getElementById('zoo-time');
    const feedButton = document.getElementById('feed-button');
    const enclosuresElement = document.getElementById('enclosures');

    function render(zoo) {
        timeElement.textContent = zoo.time.substring(0, 5);

        enclosuresElement.replaceChildren();

        ANIMAL_TYPES.forEach(function (type) {
            const heading = document.createElement('h2');
            heading.textContent = type + 's';
            enclosuresElement.appendChild(heading);

            const list = document.createElement('ol');
            zoo.animals
                .filter(function (animal) { return animal.type === type; })
                .forEach(function (animal) {
                    const item = document.createElement('li');
                    item.textContent = animal.healthPercent.toFixed(1) + '% - ' + animal.status;
                    list.appendChild(item);
                });
            enclosuresElement.appendChild(list);
        });
    }

    function refresh() {
        // TODO get the zoo from the API, then render it
        render(SAMPLE_ZOO);
    }

    function feed() {
        // TODO post to the API, then refresh
    }

    feedButton.addEventListener('click', feed);
    refresh();
})();