import { useEffect, useState } from 'react';
import './App.css';

function App() {
    const [data, setData] = useState();

    useEffect(() => {
        fetchApi();    
    }, []);
    
    async function fetchApi() {
        const response = await fetch('/api/Game/GetGames');
        const data = await response.json();
        setData(data);
    }

    return (
        <div>
            <h1 id="tabelLabel">Weather forecast</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {data ?? "test"}
        </div>
    );
    
}

export default App;