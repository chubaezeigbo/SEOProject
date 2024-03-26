import React, { useState, useEffect } from 'react';

const SearchHistory = () => {
    const [history, setHistory] = useState([]);

    useEffect(() => {
        fetch('/search/history')
            .then(response => response.json())
            .then(data => setHistory(data))
            .catch(error => console.error('Error fetching search history:', error));
    }, []);

    const handleDeleteHistory = async () => {
        try {
            const response = await fetch('/search/history', {
                method: 'DELETE'
            });
            if (response.ok) {
                setHistory([]);
            } else {
                console.error('Error deleting search history:', response.statusText);
            }
        } catch (error) {
            console.error('Error deleting search history:', error);
        }
    };

    const handleDelete = async (id) => {
        try {
            await fetch(`/search/history/${id}`, {
                method: 'DELETE'
            });
            refreshHistory();
        } catch (error) {
            console.error('Error deleting search history item:', error);
        }
    };

    const refreshHistory = () => {
        fetch('/search/history')
            .then(response => response.json())
            .then(data => setHistory(data))
            .catch(error => console.error('Error fetching search history:', error));
    };


    return (
        <div>
            <h2>Search History</h2>
            <div className="history">
                <button onClick={refreshHistory}>Update History</button>
                <button onClick={handleDeleteHistory}>Delete History</button>
            </div>
            <ul>
                {history.map((item, index) => (
                    <li key={index}>
                        <p>Keyword: {item.keyword}</p>
                        <p>URL: {item.url}</p>
                        <p>Position: {item.position}</p>
                        <p>Search Engine: {item.searchEngine}</p>
                        <button onClick={() => handleDelete(item.id)}>Delete</button>
                        <hr />
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default SearchHistory;
