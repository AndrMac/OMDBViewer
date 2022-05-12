import React, { useEffect, useState } from 'react';
import {
  Layout,
  Input,
  Row,
  Col,
  Card,
  Tag,
  Spin,
  Alert,
  Modal,
  Typography,
  Table
} from 'antd';
import 'antd/dist/antd.css';

const ImageNotFound = 'https://bitsofco.de/content/images/2018/12/broken-1.png';
const { Header, Content, Footer } = Layout;
const { Search } = Input;
const { Meta } = Card;
const TextTitle = Typography.Title;


const TopSearchTable = ({ dataSource, setQuery }) => {
  const columns = [
    {
      title: 'Search Value',
      dataIndex: 'searchValue',
      key: 'searchValue',
      render: text => <a onClick={() => setQuery(text)}>{text}</a>,
    },
    {
      title: 'Counts',
      dataIndex: 'resultsCount',
      key: 'resultsCount',
    },
    {
      title: 'Search Date',
      dataIndex: 'created',
      key: 'created',
    }
  ];
  return (
    <div style={{
      display: 'block', width: 700, padding: 30
    }}>
      <h4>Last 5 Search </h4>
      <Table dataSource={dataSource} columns={columns} pagination={false} />
    </div>

  );
}

const SearchBox = ({ searchHandler }) => {
  return (
    <Row>
      <Col span={12} offset={6}>
        <Search
          placeholder="enter movie, series, episode name"
          enterButton="Search"
          size="large"
          onSearch={value => searchHandler(value)}
        />
      </Col>
    </Row>
  )
}

const ColCardBox = ({ title, imdbID, poster, type, ShowDetail, DetailRequest, ActivateModal }) => {

  const clickHandler = () => {

    // Display Modal and Loading Icon
    ActivateModal(true);
    DetailRequest(true);

    fetch(`/details?imdbId=${imdbID}`)
      .then(resp => resp)
      .then(resp => resp.json())
      .then(response => {
        DetailRequest(false);
        ShowDetail(response);
      })
      .catch(({ message }) => {
        DetailRequest(false);
      })
  }

  return (
    <Col style={{ margin: '20px 0', minHeight: '330px' }} className="gutter-row" span={6}>
      <div className="gutter-box">
        <Card
          style={{ width: 180, minHeight: '330px' }}
          cover={
            <img
              alt={title}
              src={poster === 'N/A' ? ImageNotFound : poster}
            />
          }
          onClick={() => clickHandler()}
        >
          <Meta
            title={title}
            description={false}
          />
          <Row style={{ marginTop: '10px' }} className="gutter-row">
            <Col>
              <Tag color="magenta">{type}</Tag>
            </Col>
          </Row>
        </Card>
      </div>
    </Col>
  )
}

const MovieDetail = ({ title, poster, imdbRating, rated, runtime, genre, plot, director, actors }) => {
  return (
    <Row>
      <Col span={11}>
        <img
          src={poster === 'N/A' ? ImageNotFound : poster}
          alt={title}
        />
      </Col>
      <Col span={13}>
        <Row>
          <Col span={21}>
            <TextTitle level={4}>{title}</TextTitle></Col>
          <Col span={3} style={{ textAlign: 'right' }}>
            <TextTitle level={4}><span style={{ color: '#41A8F8' }}>{imdbRating}</span></TextTitle>
          </Col>
        </Row>
        <Row style={{ marginBottom: '20px' }}>
          <Col>
            <Tag>{rated}</Tag>
            <Tag>{runtime}</Tag>
            <Tag>{genre}</Tag>
          </Col>
        </Row>
        <Row style={{ marginBottom: '20px' }}>
          <Col>Director: {director}</Col>
        </Row>
        <Row style={{ marginBottom: '20px' }}>
          <Col>Actors: {actors}</Col>
        </Row>

        <Row>
          <Col>{plot}</Col>
        </Row>
      </Col>
    </Row>
  )
}

const Loader = () => (
  <div style={{ margin: '20px 0', textAlign: 'center' }}>
    <Spin />
  </div>
)

function App() {

  const [data, setData] = useState(null);
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(false);
  const [q, setQuery] = useState('batman');
  const [activateModal, setActivateModal] = useState(false);
  const [detail, setShowDetail] = useState(false);
  const [detailRequest, setDetailRequest] = useState(false);
  const [dataSourceStatisticSearch, setDataSourceSearch] = useState([{}]);

  useEffect(() => {

    setLoading(true);
    setError(null);
    setData(null);

    fetch(`/list?search=${q}`)
      .then(resp => resp)
      .then(resp => resp.json())
      .then(response => {
        if (response.Response === 'False') {
          setError(response.Error);
        }
        else {
          setData(response.search);
        }

        setLoading(false);
      }).then(() => {

        fetch(`/search/statistic`)
          .then(resp => resp)
          .then(resp => resp.json())
          .then(response => {
            if (response.Response === 'False') {
              setError(response.Error);
            }
            else {
              setDataSourceSearch(response);
            }
          })
      })

      .catch(({ message }) => {
        setError(message);
        setLoading(false);
      })

  }, [q]);

  return (
    <div className="App">
      <Layout className="layout">
        <Header>
          <div style={{ textAlign: 'center' }}>
            <TextTitle style={{ color: '#ffffff', marginTop: '14px' }} level={3}>OMDB Viewer</TextTitle>
          </div>
        </Header>
        <Content style={{ padding: '0 50px' }}>
          <div style={{ background: '#fff', padding: 24, minHeight: 280 }}>
            <SearchBox searchHandler={setQuery} />
            <br />

            <Row gutter={16} type="flex" justify="center">
              {loading &&
                <Loader />
              }

              {error !== null &&
                <div style={{ margin: '20px 0' }}>
                  <Alert message={error} type="error" />
                </div>
              }

              {data !== null && data.length > 0 && data.map((result, index) => (
                <ColCardBox
                  ShowDetail={setShowDetail}
                  DetailRequest={setDetailRequest}
                  ActivateModal={setActivateModal}
                  key={index}
                  {...result}
                />
              ))}
            </Row>

            <Row gutter={24} type="flex" justify="center">
              <TopSearchTable dataSource={dataSourceStatisticSearch} setQuery={setQuery}></TopSearchTable>
            </Row>
          </div>
          <Modal
            title='Detail'
            centered
            visible={activateModal}
            onCancel={() => setActivateModal(false)}
            footer={null}
            width={800}
          >
            {detailRequest === false ?
              (<MovieDetail {...detail} />) :
              (<Loader />)
            }
          </Modal>
        </Content>
        <Footer style={{ textAlign: 'center' }}>OMDB Viewer  ©2022</Footer>
      </Layout>
    </div>
  );
}

export default App;
