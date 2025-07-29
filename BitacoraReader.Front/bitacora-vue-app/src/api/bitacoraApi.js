const API_BASE = 'http://localhost:5218/api';

const apiCall = async (endpoint, options = {}) => {
    try {
        const response = await fetch(`${API_BASE}${endpoint}`, {
            headers: {
                'Content-Type': 'application/json',
                ...options.headers
            },
            ...options
        });
        
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        
        return await response.json();
    } catch (error) {
        console.error('API call failed:', error);
        throw error;
    }
};

const bitacoraApi = {
    getTotalCount: async () => {
        return await apiCall('/bitacora/total-count');
    },
    getDistinctJobs: async () => {
        return await apiCall('/bitacora/distinct-jobs');
    },
    getDistinctResultados: async () => {
        return await apiCall('/bitacora/distinct-resultados');
    },
    getBitacoras: async (filters) => {
        const queryParams = new URLSearchParams();
        
        Object.entries(filters).forEach(([key, value]) => {
            if (value !== '' && value !== null && value !== undefined) {
                queryParams.append(key, value);
            }
        });

        return await apiCall(`/bitacora?${queryParams.toString()}`);
    }
};

export default bitacoraApi;