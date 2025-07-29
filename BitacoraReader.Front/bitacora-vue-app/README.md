# Bitacora Vue App

## Overview
The Bitacora Vue App is a web application designed to manage and display bitacoras (logs) with various functionalities such as filtering, viewing details, and pagination. This application is built using Vue.js and utilizes Vite as the build tool.

## Project Structure
```
bitacora-vue-app
├── public
│   └── index.html          # Main HTML file for the Vue application
├── src
│   ├── assets              # Directory for static assets (images, fonts, styles)
│   ├── components          # Vue components for the application
│   │   ├── BitacoraTable.vue        # Component for displaying the bitacora table
│   │   ├── BitacoraDetailModal.vue   # Component for displaying bitacora details in a modal
│   │   ├── FiltersPanel.vue          # Component for filtering bitacoras
│   │   ├── StatsCards.vue            # Component for displaying statistics
│   │   └── Pagination.vue             # Component for pagination controls
│   ├── App.vue              # Root component of the Vue application
│   ├── main.js              # Entry point of the Vue application
│   └── api
│       └── bitacoraApi.js   # API calls related to bitacoras
├── package.json             # npm configuration file
├── README.md                # Project documentation
└── vite.config.js           # Vite configuration file
```

## Setup Instructions
1. **Clone the repository:**
   ```
   git clone <repository-url>
   cd bitacora-vue-app
   ```

2. **Install dependencies:**
   ```
   npm install
   ```

3. **Run the application:**
   ```
   npm run dev
   ```

4. **Open your browser and navigate to:**
   ```
   http://localhost:3000
   ```

## Usage
- Use the filters to search for specific bitacoras.
- Click on the "View" button in the table to see detailed information about a bitacora in a modal.
- Navigate through the pages using the pagination controls.

## Contributing
Contributions are welcome! Please feel free to submit a pull request or open an issue for any enhancements or bug fixes.

## License
This project is licensed under the MIT License. See the LICENSE file for more details.