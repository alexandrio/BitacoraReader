import { createApp, ref, computed, onMounted } from 'vue';
import FiltersPanel from './components/FiltersPanel.vue';
import StatsCards from './components/StatsCards.vue';
import BitacoraTable from './components/BitacoraTable.vue';
import Pagination from './components/Pagination.vue';
import BitacoraDetailModal from './components/BitacoraDetailModal.vue';
import { testConnection, loadTotalCount, loadDistinctJobs, loadDistinctResultados, loadBitacoras } from './api/bitacoraApi.js';

export default {
  components: {
    FiltersPanel,
    StatsCards,
    BitacoraTable,
    Pagination,
    BitacoraDetailModal
  },
  setup() {
    const loading = ref(false);
    const connectionStatus = ref(false);
    const totalRecords = ref(0);
    const distinctJobs = ref([]);
    const distinctResultados = ref([]);
    const pagedResult = ref({
      items: [],
      totalCount: 0,
      pageNumber: 1,
      pageSize: 50,
      totalPages: 0,
      hasPreviousPage: false,
      hasNextPage: false
    });
    const selectedBitacora = ref(null);
    const filters = ref({
      idJob: '',
      dia: '',
      fechaInicio: '',
      fechaTermino: '',
      resultado: '',
      nombreArchivo: '',
      pageNumber: 1,
      pageSize: 50
    });

    const loadInitialData = async () => {
      loading.value = true;
      try {
        await Promise.all([
          testConnection(),
          loadTotalCount(),
          loadDistinctJobs(),
          loadDistinctResultados(),
          loadBitacoras()
        ]);
      } catch (error) {
        console.error('Error loading initial data:', error);
      } finally {
        loading.value = false;
      }
    };

    const applyFilters = () => {
      filters.value.pageNumber = 1;
      loadBitacoras();
    };

    const clearFilters = () => {
      filters.value = {
        idJob: '',
        dia: '',
        fechaInicio: '',
        fechaTermino: '',
        resultado: '',
        nombreArchivo: '',
        pageNumber: 1,
        pageSize: filters.value.pageSize
      };
      loadBitacoras();
    };

    const goToPage = (page) => {
      filters.value.pageNumber = page;
      loadBitacoras();
    };

    const viewDetail = (bitacora) => {
      selectedBitacora.value = bitacora;
    };

    onMounted(() => {
      loadInitialData();
    });

    return {
      loading,
      connectionStatus,
      totalRecords,
      distinctJobs,
      distinctResultados,
      pagedResult,
      selectedBitacora,
      filters,
      applyFilters,
      clearFilters,
      goToPage,
      viewDetail
    };
  }
};