import React, { useState } from 'react';

const SearchForm = ({ onSubmit }) => {
    const [keyword, setKeyword] = useState('');
    const [url, setUrl] = useState('');
    const [engine, setEngine] = useState('google');

    const handleSubmit = (e) => {
        e.preventDefault();
        onSubmit(keyword, url, engine);
    };

    return (
        <form onSubmit={handleSubmit}>
            <div className="input-container">
                <label>
                    Keyword:
                    <input
                        type="text"
                        id="keyword"
                        value={keyword}
                        onChange={(e) => setKeyword(e.target.value)}
                        required
                    />
                </label>
                <label>
                    URL:
                    <input
                        type="text"
                        id="url"
                        value={url}
                        onChange={(e) => setUrl(e.target.value)}
                        required
                    />
                </label>
            </div>
            <div className="radio-container">
                <label htmlFor="radio1">
                    Google
                    <input
                        type="radio"
                        id="radio1"
                        name="engine"
                        value="google"
                        onChange={(e) => setEngine(e.target.value)}
                        checked={engine === 'google'}
                    />
                </label>
                <label htmlFor="radio2">
                    Bing
                    <input
                        type="radio"
                        id="radio2"
                        name="engine"
                        value="bing"
                        onChange={(e) => setEngine(e.target.value)}
                        checked={engine === 'bing'}
                    />
                </label>
            </div>
            <div className="button-container">
                <button type="submit">Search</button>
            </div>
            
        </form>
    );
};

export default SearchForm;
