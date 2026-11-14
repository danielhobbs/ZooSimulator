(function () {
    'use strict';

    const ZOO_URL = 'api/zoo';
    const FEED_URL = 'api/zoo/feed';
    const POLL_INTERVAL_MS = 5000; // 5 seconds = every "15" mins

    const ANIMAL_TYPES = ['Elephant', 'Giraffe', 'Monkey'];

    const timeElement = document.getElementById('zoo-time');
    const feedButton = document.getElementById('feed-button');
    const messageElement = document.getElementById('message');
    const enclosuresElement = document.getElementById('enclosures');

    function showMessage(text) {
        messageElement.textContent = text;
        messageElement.hidden = false;
    }

    function clearMessage() {
        messageElement.hidden = true;
    }

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

    async function refresh() {
        try {
            const response = await fetch(ZOO_URL);
            if (!response.ok) {
                throw new Error('Unexpected response ' + response.status);
            }
            render(await response.json());
            clearMessage();
        } catch (error) {
            showMessage('Cannot reach the zoo. Retrying...');
        }
    }

    async function feed() {
        feedButton.disabled = true;

        try {
            const response = await fetch(FEED_URL, { method: 'POST' });
            if (!response.ok) {
                throw new Error('Unexpected response ' + response.status);
            }
            await refresh();
        } catch (error) {
            showMessage('Feeding failed. Please try again.');
        } finally {
            feedButton.disabled = false;
        }
    }

    feedButton.addEventListener('click', feed);
    refresh();
    setInterval(refresh, POLL_INTERVAL_MS);
})();