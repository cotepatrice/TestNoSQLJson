<template>
  <v-container fluid>
    <v-slide-y-transition mode="out-in">
      <v-row>
        <v-col>
          <h1>Profil investisseur</h1>
          <p>This component demonstrates fetching profils investisseur from the .Net 5 web Api.</p>

          <v-data-table
            :headers="headers"
            :items="profilInvestisseur"
            hide-default-footer
            :loading="loading"
            class="elevation-1"
          >
            <template v-slot:progress>
              <v-progress-linear color="blue" indeterminate></v-progress-linear>
            </template>
            <template v-slot:[`item.creationDate`]="{ item }">
              <td>{{ item.creationDate | date }}</td>
            </template>
            <template v-slot:[`item.subscriberId`]="{ item }">
              <td>{{ item.subscriberId }}</td>
            </template>
            <template v-slot:[`item.profilInvestisseurId`]="{ item }">
              <td>{{ item.profilInvestisseurId }}</td>
            </template>
            <!-- <template v-slot:[`item.temperatureC`]="{ item }">
              <v-chip :color="getColor(item.temperatureC)" dark>{{ item.temperatureC }}</v-chip>
            </template> -->
          </v-data-table>
        </v-col>
      </v-row>
    </v-slide-y-transition>

    <v-alert :value="showError" type="error" v-text="errorMessage">
      This is an error alert.
    </v-alert>

    <v-alert :value="showError" type="warning">
      Are you sure you're using ASP.NET Core endpoint? (default at
      <a href="https://localhost:44365/api/ProfilInvestisseur/1">https://localhost:44365/api/ProfilInvestisseur/1</a>)
      <br />
      API call would fail with status code 404 when calling from Vue app (default at
      <a href="http://localhost:8080/fetch-profil">http://localhost:8080</a>) without devServer proxy
      settings in vue.config.js file.
    </v-alert>
  </v-container>
</template>

<script lang="ts">
// an example of a Vue Typescript component using Vue.extend
import Vue from 'vue'
import { Forecast } from '../models/Forecast'
import { ProfilInvestisseur } from '../models/ProfilInvestisseur'

export default Vue.extend({
  data() {
    return {
      loading: true,
      showError: false,
      errorMessage: 'Error while loading weather forecast.',
      profilInvestisseur: [] as ProfilInvestisseur[],
      headers: [
        { text: 'Creation Date', value: 'creationDate' },
        { text: 'Subscriber Id', value: 'subscriberId' },
        { text: 'Id Profil', value: 'profilInvestisseurId' },
        // { text: 'Summary', value: 'summary' }
      ]
    }
  },
  methods: {
    getColor(temperature: number) {
      if (temperature < 0) {
        return 'blue'
      } else if (temperature >= 0 && temperature < 30) {
        return 'green'
      } else {
        return 'red'
      }
    },
    async fetchProfils() {
      try {
        const response = await this.$axios.get<ProfilInvestisseur[]>('api/ProfilInvestisseur/1')
        this.profilInvestisseur = response.data
      } catch (e) {
        this.showError = true
        this.errorMessage = `Error while loading weather forecast: ${e.message}.`
      }
      this.loading = false
    }
  },
  async created() {
    await this.fetchProfils()
  }
})
</script>
