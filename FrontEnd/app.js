const API_URL = 'https://localhost:7204'; // Adjust as needed
        const gameTable = document.getElementById('gameTable').getElementsByTagName('tbody')[0];
        const gameFormPopup = document.getElementById('gameFormPopup');
        const overlay = document.getElementById('overlay');
        const gameForm = document.getElementById('gameForm');
        const genreSelect = document.getElementById('genre');
        const addGameButton = document.getElementById('addGameButton');
        const cancelButton = document.getElementById('cancelButton');

        let currentGameId = null; // Track the game being updated

        // Fetch and display genres for dropdown
        async function loadGenres() {
            try {
                const response = await fetch(`${API_URL}/genre`);
                const genres = await response.json();
                genreSelect.innerHTML = ''; // Clear existing options
                genres.forEach(genre => {
                    const option = document.createElement('option');
                    option.value = genre.id;
                    option.textContent = genre.name;
                    genreSelect.appendChild(option);
                });
            } catch (error) {
                console.error('Error loading genres:', error);
            }
        }

        // Fetch and display games
        async function loadGames() {
            try {
                const response = await fetch(`${API_URL}/games`);
                const games = await response.json();
                gameTable.innerHTML = ''; // Clear table
                games.forEach(game => {
                    const row = gameTable.insertRow();
                    row.insertCell(0).textContent = game.name;
                    row.insertCell(1).textContent = `$${game.price.toFixed(2)}`;
                    row.insertCell(2).textContent = new Date(game.relaseDate).toLocaleString();
                    row.insertCell(3).textContent = game.genre || 'N/A';

                    // Add action buttons
                    const actionsCell = row.insertCell(4);
                    const updateButton = document.createElement('button');
                    updateButton.textContent = 'Update';
                    updateButton.className = 'btn btn-update';
                    updateButton.onclick = () => showGameForm(game);

                    const deleteButton = document.createElement('button');
                    deleteButton.textContent = 'Delete';
                    deleteButton.className = 'btn btn-delete';
                    deleteButton.onclick = () => deleteGame(game.id);

                    actionsCell.appendChild(updateButton);
                    actionsCell.appendChild(deleteButton);
                });
            } catch (error) {
                console.error('Error loading games:', error);
            }
        }

        // Show game form
        function showGameForm(game = null) {
            currentGameId = game ? game.id : null;
            if (game) {
                document.getElementById('name').value = game.name;
                document.getElementById('price').value = game.price;
                document.getElementById('releaseDate').value = game.relaseDate;
                document.getElementById('genre').value = game.genreId;
            } else {
                gameForm.reset();
            }
            gameFormPopup.style.display = 'block';
            overlay.style.display = 'block';
        }

        // Hide game form
        function hideGameForm() {
            gameFormPopup.style.display = 'none';
            overlay.style.display = 'none';
        }

        // Delete a game
        async function deleteGame(id) {
            try {
                const response = await fetch(`${API_URL}/games/${id}`, {
                    method: 'DELETE'
                });
                if (response.ok) {
                    alert('Game deleted successfully');
                    loadGames();
                } else {
                    alert('Error deleting game');
                }
            } catch (error) {
                console.error('Error deleting game:', error);
            }
        }

        // Add or update a game
        gameForm.addEventListener('submit', async (e) => {
            e.preventDefault();
            const game = {
                name: document.getElementById('name').value,
                price: parseFloat(document.getElementById('price').value),
                relaseDate: document.getElementById('releaseDate').value,
                genreId: parseInt(document.getElementById('genre').value)
            };

            try {
                const method = currentGameId ? 'PUT' : 'POST';
                const endpoint = currentGameId ? `${API_URL}/games/${currentGameId}` : `${API_URL}/games`;
                const response = await fetch(endpoint, {
                    method,
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(game)
                });
                if (response.ok) {
                    alert('Game saved successfully');
                    hideGameForm();
                    loadGames();
                } else {
                    alert('Error saving game');
                }
            } catch (error) {
                console.error('Error saving game:', error);
            }
        });

        // Event listeners
        addGameButton.addEventListener('click', () => showGameForm());
        cancelButton.addEventListener('click', hideGameForm);

        // Initialize the page
        async function initialize() {
            await loadGenres();
            await loadGames();
        }

        initialize();