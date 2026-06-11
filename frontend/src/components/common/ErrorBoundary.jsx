import { Component } from 'react';
import ErrorState from './ErrorState';

class ErrorBoundary extends Component {
  constructor(props) {
    super(props);
    this.state = { hasError: false, error: null };
  }

  static getDerivedStateFromError(error) {
    return { hasError: true, error };
  }

  componentDidCatch(error, info) {
    console.error('Unhandled React error', error, info);
  }

  reset = () => {
    this.setState({ hasError: false, error: null });
  };

  render() {
    if (this.state.hasError) {
      return (
        <div className="p-6">
          <ErrorState
            title="Unexpected application error"
            description={this.state.error?.message}
            onRetry={this.reset}
          />
        </div>
      );
    }

    return this.props.children;
  }
}

export default ErrorBoundary;
