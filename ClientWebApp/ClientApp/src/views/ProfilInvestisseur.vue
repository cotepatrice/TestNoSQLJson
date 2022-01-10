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
            :expanded.sync="expanded"
            show-expand
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
            <template v-slot:expanded-item="{ headers, item }">
              <td :colspan="headers.length">
                <ul id="example-1">
                  <li v-for="content in item.content" :key=getUniqueKey(content)>
                    {{ content.labelText }} : {{ getContentValue(content.dotNetProfileModelType, content.value) }}
                  </li>
                </ul>
              </td>
            </template>
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
      <a href="http://localhost:8080/fetch-profil">http://localhost:8080/fetch-profil</a>) without devServer proxy
      settings in vue.config.js file.
    </v-alert>
  </v-container>
</template>

<script lang="ts">
// an example of a Vue Typescript component using Vue.extend
import { ProfilLine } from '@/models/ProfilLine'
import Vue from 'vue'
import { ProfilInvestisseur } from '../models/ProfilInvestisseur'
import { ComplexLineValue } from '../models/ComplexLineValue'

export default Vue.extend({
  data() {
    return {
      expanded: [] as ProfilInvestisseur[],
      // singleExpand: false,
      loading: true,
      showError: false,
      errorMessage: 'Error while loading weather forecast.',
      profilInvestisseur: [] as ProfilInvestisseur[],
      headers: [
        { text: 'Creation Date', value: 'creationDate' },
        { text: 'Subscriber Id', value: 'subscriberId' },
        { text: 'Id Profil', value: 'profilInvestisseurId' },
        { text: '', value: 'data-table-expand' },
      ]
    }
  },
  methods: {
    expandAllIssues(items: ProfilInvestisseur[], status: boolean) {
      if (status) {
        this.expanded = items
      } else {
        this.expanded = []
      }
    },
    getUniqueKey(item: ProfilLine){
      return item.labelText + "-" + item.labelVersion
   },
    async fetchProfils() {
      try {
        const response = await this.$axios.get<ProfilInvestisseur[]>('api/ProfilInvestisseur/1')
        this.profilInvestisseur = response.data
      } catch (e) {
        this.showError = true
        var error = e as Error
        this.errorMessage = `Error while loading weather forecast: ${error.message}.`
      }
      this.loading = false
    },
    getContentValue(dotNetProfileModelType: number, value: string) {
      if(dotNetProfileModelType === 4){
        var profileLineValue = JSON.parse(value) as ProfilLine[];
        var response = ""
        profileLineValue.forEach(element => {
          console.log(`element.labelText = ${element.labelText}`)
          response += ` | ${element.labelText} : ${this.getContentValue(element.dotNetProfileModelType as number, element.value as string)}`
        });
        return response;
      }
      if(dotNetProfileModelType === 5){
        var complexLineValue = JSON.parse(value) as ComplexLineValue 
        return `${complexLineValue.numberOfUnits} ${complexLineValue.unitName}`
      }

      return value;
    }
  },
  async created() {
    await this.fetchProfils();
    this.expandAllIssues(this.profilInvestisseur, true);
  }
})
</script>
