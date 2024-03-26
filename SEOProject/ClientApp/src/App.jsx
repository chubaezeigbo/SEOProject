
import React, { useState } from 'react';
import SearchForm from './components/SearchForm';
import SearchResult from './components/SearchResults';
import SearchHistory from './components/SearchHistory';

const App = () => {
    const [result, setResult] = useState(null);
    const [error, setError] = useState(null);

    const handleSubmit = async (keyword, url, engine) => {
        try {
            const response = await fetch(`/search/${engine}/${keyword}/${url}`);
            const data = await response.json();
          
            setResult(data);
            setError(null);
        } catch (error) {
            setError('An error occurred while processing the request.');
            setResult(null);
        }
    };

    return (
        <div>
            <h1>InfoTrack WebScraper</h1>
            <SearchForm onChange={(e) => e.target.value} onSubmit={handleSubmit} />
            {error && <p>{error}</p>}
            {result && <SearchResult result={result} />}
            <SearchHistory />
        </div>
    );
};

export default App;
