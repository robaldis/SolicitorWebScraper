<script setup lang="ts">
import { ref, onMounted } from 'vue';
import LocationEditor from './components/LocationEditor.vue';
import ResultsTable from './components/ResultsTable.vue';
import { fetchLocations, searchSolicitors } from './api/searchApi';
import type { Solicitor } from './types/solicitor';

const locations = ref<string[]>([]);

onMounted(async () => {
  try {
    locations.value = await fetchLocations();
  } catch {
    locations.value = ['london'];
  }
});

const solicitors = ref<Solicitor[]>([]);
const loading = ref(false);
const error = ref<string | null>(null);

async function runSearch() {
  loading.value = true;
  error.value = null;
  try {
    solicitors.value = await searchSolicitors(locations.value);
  } catch (e) {
    error.value = e instanceof Error ? e.message : 'Search failed';
  } finally {
    loading.value = false;
  }
}
</script>

<template>
  <h1>InfoTrack Conveyancing Solicitor Finder</h1>
  <LocationEditor :locations="locations" @update="locations = $event" />
  <button @click="runSearch" :disabled="loading">
    {{ loading ? 'Searching...' : 'Search' }}
  </button>
  <div v-if="loading" class="spinner"></div>
  <p v-if="error" style="color: red">{{ error }}</p>
  <ResultsTable :solicitors="solicitors" />
</template>

<style>
.spinner {
  width: 32px;
  height: 32px;
  border: 4px solid #ddd;
  border-top-color: #4a90d9;
  border-radius: 50%;
  animation: spin 0.8s linear infinite;
  margin: 16px 0;
}

@keyframes spin {
  to { transform: rotate(360deg); }
}
</style>
