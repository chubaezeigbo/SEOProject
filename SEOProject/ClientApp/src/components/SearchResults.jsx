import React from 'react';

const SearchResult = ({ result }) => {
    return (
        <div>
            <h2>Search Result</h2>
            <p>Position: {result.position}</p>
            <p>Keyword: {result.keyword}</p>
            <p>URL: {result.url}</p>
            <p>Search Engine: {result.searchEngine}</p>
        </div>
    );
};

export default SearchResult;
