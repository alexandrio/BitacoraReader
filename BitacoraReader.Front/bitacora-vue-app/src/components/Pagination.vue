<template>
  <div class="d-flex justify-content-between align-items-center mt-4" v-if="totalCount > 0">
    <div>
      Mostrando {{ startRecord }} - {{ endRecord }} de {{ totalCount.toLocaleString() }} registros
    </div>
    <nav>
      <ul class="pagination">
        <li class="page-item" :class="{ disabled: !hasPreviousPage }">
          <button class="page-link" @click="goToPage(currentPage - 1)" :disabled="!hasPreviousPage">
            <i class="bi bi-chevron-left"></i>
          </button>
        </li>
        <li v-for="page in visiblePages" :key="page" class="page-item" :class="{ active: page === currentPage }">
          <button class="page-link" @click="goToPage(page)">{{ page }}</button>
        </li>
        <li class="page-item" :class="{ disabled: !hasNextPage }">
          <button class="page-link" @click="goToPage(currentPage + 1)" :disabled="!hasNextPage">
            <i class="bi bi-chevron-right"></i>
          </button>
        </li>
      </ul>
    </nav>
  </div>
</template>

<script>
export default {
  props: {
    totalCount: {
      type: Number,
      required: true
    },
    currentPage: {
      type: Number,
      required: true
    },
    pageSize: {
      type: Number,
      required: true
    },
    hasPreviousPage: {
      type: Boolean,
      required: true
    },
    hasNextPage: {
      type: Boolean,
      required: true
    },
    visiblePages: {
      type: Array,
      required: true
    }
  },
  computed: {
    startRecord() {
      return (this.currentPage - 1) * this.pageSize + 1;
    },
    endRecord() {
      return Math.min(this.currentPage * this.pageSize, this.totalCount);
    }
  },
  methods: {
    goToPage(page) {
      this.$emit('page-changed', page);
    }
  }
};
</script>

<style scoped>
.pagination {
  margin: 0;
}
</style>