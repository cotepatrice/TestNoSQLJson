module.exports = {
  configureWebpack: {
    devtool: 'source-map'
  },
  transpileDependencies: ['vuetify'],
  devServer: {
    progress: !!process.env.VUE_DEV_SERVER_PROGRESS
  },
  devServer: {
    proxy: {
      '^/api': {
        target: 'https://localhost:44365',
        changeOrigin: true
      },
    }
  }
}
