(function () {
    'use strict';

    const ZOO_URL = 'api/zoo';
    const FEED_URL = 'api/zoo/feed';
    const POLL_INTERVAL_MS = 5000; // 5 seconds = every "15" mins

    const ANIMAL_TYPES = ['Elephant', 'Giraffe', 'Monkey'];
    const TYPE_HEADINGS = { Elephant: 'Elephants', Giraffe: 'Giraffes', Monkey: 'Monkeys' };
    const STATUS_LABELS = { Alive: 'Alive', CannotWalk: 'Cannot walk', Dead: 'Dead' };

    const timeElement = document.getElementById('zoo-time');
    const feedButton = document.getElementById('feed-button');
    const messageElement = document.getElementById('message');
    const enclosuresElement = document.getElementById('enclosures');

    let refreshInProgress = false;

    function formatTime(time) {
        if (typeof time !== 'string' || time.length < 5) {
            return '--:--';
        }
        return time.substring(0, 5);
    }

    function showMessage(text) {
        messageElement.textContent = text;
        messageElement.hidden = false;
    }

    function clearMessage() {
        messageElement.hidden = true;
        messageElement.textContent = '';
    }

    function createElement(tagName, className, text) {
        const element = document.createElement(tagName);
        if (className) {
            element.className = className;
        }
        if (text !== undefined) {
            element.textContent = text;
        }
        return element;
    }

    function createAnimalRow(animal, number) {
        const status = animal.status;
        const health = Number(animal.healthPercent);

        const row = createElement('li', 'animal status-' + status);
        row.appendChild(createElement('span', 'animal-number', String(number)));

        const bar = createElement('span', 'health-bar');
        const fill = createElement('span', 'health-bar-fill');
        fill.style.width = Math.max(0, Math.min(100, health)) + '%';
        bar.appendChild(fill);
        row.appendChild(bar);

        row.appendChild(createElement('span', 'animal-health', health.toFixed(1) + '%'));
        row.appendChild(createElement('span', 'animal-status', STATUS_LABELS[status] || status));

        return row;
    }

    function createEnclosure(type, animals) {
        const section = createElement('section', 'enclosure');
        section.appendChild(createElement('h2', null, TYPE_HEADINGS[type] || type));

        const list = createElement('ol', 'animal-list');
        animals.forEach(function (animal, index) {
            list.appendChild(createAnimalRow(animal, index + 1));
        });
        section.appendChild(list);

        return section;
    }

    function render(zoo) {
        timeElement.textContent = formatTime(zoo.time);

        const animals = zoo.animals || [];
        const enclosures = ANIMAL_TYPES.map(function (type) {
            const animalsOfType = animals.filter(function (animal) {
                return animal.type === type;
            });
            return createEnclosure(type, animalsOfType);
        });

        enclosuresElement.replaceChildren(...enclosures);
    }

    async function refresh() {
        if (refreshInProgress) {
            return;
        }
        refreshInProgress = true;

        try {
            const response = await fetch(ZOO_URL, { headers: { Accept: 'application/json' } });
            if (!response.ok) {
                throw new Error('Unexpected response ' + response.status);
            }
            render(await response.json());
            clearMessage();
        } catch (error) {
            showMessage('Cannot reach the zoo. Retrying...');
        } finally {
            refreshInProgress = false;
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
