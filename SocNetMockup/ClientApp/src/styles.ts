import {createGlobalStyle} from "styled-components";

export const Styles = createGlobalStyle`
  :root {
    background-color: var(--background-color);
    color: var(--text-color);
  }
  
  :root, .light-theme:root {
    --text-color: black;
    --background-color: white;
    --link-color: #0366d6;

    --btn-primary-color: #fff;
    --btn-primary-background-color: #1b6ec2;
    --btn-primary-border-color: #1861ac;
  }

  .dark-theme:root {
    --text-color: #efefef;
    --background-color: #1c2424;
    --link-color: #0366d6;

    --btn-primary-color: #fff;
    --btn-primary-background-color: #1b6ec2;
    --btn-primary-border-color: #1861ac;
  }
  
  body {
    background-color: var(--background-color);
  }

  a {
    color: var(--link-color);
  }

  code {
    color: #E01A76;
  }

  .btn-primary {
    color: var(--btn-primary-color);
    background-color: var(--btn-primary-background-color);
    border-color: var(--btn-primary-border-color);
  }

  /*
 Messenger
 */

  #MessengerPage {
    display: flex;
    gap: 1em;
    flex-grow: 1;
    height: calc(100vh - 128px);
  }

  #PeerList {
    min-width: 200px;
    width: 30%;
    max-width: 280px;
    display: flex;
    flex-direction: column;
    gap: 8px;
  }

  .peer-list-item {
    border: 1px solid #dedede;
    padding: 0.4em 0.4em;
    display: flex;
    align-items: center;

    &.selected {
      background-color: #fafafa;
    }

    .image img {
      width: 48px;
      height: 48px;
      margin: 8px;
    }

  }

  #ChatWindow {
    border: 1px solid #dedede;
    flex-grow: 1;

    display: flex;
    flex-direction: column;

    #ChatWindowHeader {
      background-color: #fafafa;
      border-bottom: 1px solid #dedede;
      padding: 1em 0.5em;
    }

    #ChatHistory {
      flex-grow: 1;
    }

    #ChatWindowFooter {
      background-color: #fafafa;
      border-top: 1px solid #dedede;
      padding: 1em 0.5em;
    }
  }
`;