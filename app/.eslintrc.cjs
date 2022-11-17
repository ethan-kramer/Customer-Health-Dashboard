module.exports = {
  root: true,
  env: {
    node: true,
    browser: true,
    es6: true,
  },
  extends: ['eslint:recommended', 'plugin:react/recommended', 'eslint-config-prettier'],
  parserOptions: {
    ecmaVersion: 11,
    sourceType: 'module',
  },
  globals: {
    __APP_VERSION__: 'readonly',
  },
  settings: {
    react: {
      version: 'detect',
    },
    'import/resolver': {
      node: {
        paths: ['src'],
        extensions: ['.js', '.jsx'],
      },
    },
  },
  rules: {
    // override/add rules settings here, such as:

    'vue/no-unused-vars': 'error'
    'react/react-in-jsx-scope': 'off',
    'react/prop-types': 'warn',
    'empty-brace-spaces': 'warn', //Enforce no spaces between braces.
  },
};

