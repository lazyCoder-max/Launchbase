console.log("Wallet script loaded");

const ABI = [
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "target",
                "type": "address"
            }
        ],
        "name": "AddressEmptyCode",
        "type": "error"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "tokenAddress",
                "type": "address"
            },
            {
                "internalType": "uint256",
                "name": "tokenAmount",
                "type": "uint256"
            },
            {
                "internalType": "bool",
                "name": "isToken",
                "type": "bool"
            },
            {
                "internalType": "uint256",
                "name": "poolId",
                "type": "uint256"
            }
        ],
        "name": "contribute",
        "outputs": [],
        "stateMutability": "payable",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "_token",
                "type": "address"
            },
            {
                "internalType": "address",
                "name": "_adminWallet",
                "type": "address"
            },
            {
                "internalType": "uint256",
                "name": "_startTime",
                "type": "uint256"
            },
            {
                "internalType": "uint256",
                "name": "_endTime",
                "type": "uint256"
            },
            {
                "internalType": "uint256",
                "name": "_softCap",
                "type": "uint256"
            },
            {
                "internalType": "uint256",
                "name": "_hardCap",
                "type": "uint256"
            },
            {
                "internalType": "uint256",
                "name": "_minContribution",
                "type": "uint256"
            },
            {
                "internalType": "uint256",
                "name": "_maxContribution",
                "type": "uint256"
            },
            {
                "internalType": "string[3]",
                "name": "_otherInfo",
                "type": "string[3]"
            },
            {
                "internalType": "address[]",
                "name": "tokens",
                "type": "address[]"
            },
            {
                "internalType": "uint256[]",
                "name": "rates",
                "type": "uint256[]"
            },
            {
                "internalType": "bool[]",
                "name": "isTokens",
                "type": "bool[]"
            }
        ],
        "name": "createPool",
        "outputs": [],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "implementation",
                "type": "address"
            }
        ],
        "name": "ERC1967InvalidImplementation",
        "type": "error"
    },
    {
        "inputs": [],
        "name": "ERC1967NonPayable",
        "type": "error"
    },
    {
        "inputs": [],
        "name": "FailedInnerCall",
        "type": "error"
    },
    {
        "inputs": [],
        "name": "InvalidInitialization",
        "type": "error"
    },
    {
        "inputs": [],
        "name": "NotInitializing",
        "type": "error"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "owner",
                "type": "address"
            }
        ],
        "name": "OwnableInvalidOwner",
        "type": "error"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "account",
                "type": "address"
            }
        ],
        "name": "OwnableUnauthorizedAccount",
        "type": "error"
    },
    {
        "inputs": [],
        "name": "UUPSUnauthorizedCallContext",
        "type": "error"
    },
    {
        "inputs": [
            {
                "internalType": "bytes32",
                "name": "slot",
                "type": "bytes32"
            }
        ],
        "name": "UUPSUnsupportedProxiableUUID",
        "type": "error"
    },
    {
        "anonymous": false,
        "inputs": [
            {
                "indexed": true,
                "internalType": "uint256",
                "name": "transactionId",
                "type": "uint256"
            },
            {
                "indexed": true,
                "internalType": "address",
                "name": "contributor",
                "type": "address"
            },
            {
                "indexed": false,
                "internalType": "uint256",
                "name": "amount",
                "type": "uint256"
            }
        ],
        "name": "Contribution",
        "type": "event"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "_owner",
                "type": "address"
            }
        ],
        "name": "initialize",
        "outputs": [],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "anonymous": false,
        "inputs": [
            {
                "indexed": false,
                "internalType": "uint64",
                "name": "version",
                "type": "uint64"
            }
        ],
        "name": "Initialized",
        "type": "event"
    },
    {
        "anonymous": false,
        "inputs": [
            {
                "indexed": true,
                "internalType": "address",
                "name": "previousOwner",
                "type": "address"
            },
            {
                "indexed": true,
                "internalType": "address",
                "name": "newOwner",
                "type": "address"
            }
        ],
        "name": "OwnershipTransferred",
        "type": "event"
    },
    {
        "anonymous": false,
        "inputs": [
            {
                "indexed": true,
                "internalType": "uint256",
                "name": "transactionId",
                "type": "uint256"
            },
            {
                "indexed": true,
                "internalType": "address",
                "name": "admin",
                "type": "address"
            },
            {
                "indexed": false,
                "internalType": "uint256",
                "name": "startTime",
                "type": "uint256"
            },
            {
                "indexed": false,
                "internalType": "uint256",
                "name": "endTime",
                "type": "uint256"
            }
        ],
        "name": "PoolInitialized",
        "type": "event"
    },
    {
        "inputs": [],
        "name": "renounceOwnership",
        "outputs": [],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "newOwner",
                "type": "address"
            }
        ],
        "name": "transferOwnership",
        "outputs": [],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "anonymous": false,
        "inputs": [
            {
                "indexed": true,
                "internalType": "address",
                "name": "implementation",
                "type": "address"
            }
        ],
        "name": "Upgraded",
        "type": "event"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "newImplementation",
                "type": "address"
            },
            {
                "internalType": "bytes",
                "name": "data",
                "type": "bytes"
            }
        ],
        "name": "upgradeToAndCall",
        "outputs": [],
        "stateMutability": "payable",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "uint256",
                "name": "",
                "type": "uint256"
            }
        ],
        "name": "allContributors",
        "outputs": [
            {
                "internalType": "uint256",
                "name": "poolId",
                "type": "uint256"
            },
            {
                "internalType": "address",
                "name": "contributor",
                "type": "address"
            },
            {
                "internalType": "uint256",
                "name": "amount",
                "type": "uint256"
            },
            {
                "internalType": "uint256",
                "name": "timestamp",
                "type": "uint256"
            },
            {
                "internalType": "address",
                "name": "currencyUsed",
                "type": "address"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "uint256",
                "name": "_poolid",
                "type": "uint256"
            }
        ],
        "name": "checkPoolState",
        "outputs": [
            {
                "internalType": "enum Launchbase.PoolState",
                "name": "",
                "type": "uint8"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "uint256",
                "name": "",
                "type": "uint256"
            },
            {
                "internalType": "uint256",
                "name": "",
                "type": "uint256"
            }
        ],
        "name": "contributorsByPoolId",
        "outputs": [
            {
                "internalType": "uint256",
                "name": "",
                "type": "uint256"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "getAllPools",
        "outputs": [
            {
                "components": [
                    {
                        "internalType": "uint256",
                        "name": "id",
                        "type": "uint256"
                    },
                    {
                        "internalType": "address",
                        "name": "token",
                        "type": "address"
                    },
                    {
                        "internalType": "address",
                        "name": "adminWallet",
                        "type": "address"
                    },
                    {
                        "internalType": "uint256",
                        "name": "startTime",
                        "type": "uint256"
                    },
                    {
                        "internalType": "uint256",
                        "name": "endTime",
                        "type": "uint256"
                    },
                    {
                        "internalType": "uint256",
                        "name": "totalRaised",
                        "type": "uint256"
                    },
                    {
                        "internalType": "uint256",
                        "name": "softCap",
                        "type": "uint256"
                    },
                    {
                        "internalType": "uint256",
                        "name": "hardCap",
                        "type": "uint256"
                    },
                    {
                        "internalType": "enum Launchbase.PoolState",
                        "name": "poolState",
                        "type": "uint8"
                    },
                    {
                        "internalType": "uint256",
                        "name": "minContribution",
                        "type": "uint256"
                    },
                    {
                        "internalType": "uint256",
                        "name": "maxContribution",
                        "type": "uint256"
                    },
                    {
                        "internalType": "string[3]",
                        "name": "_otherInfo",
                        "type": "string[3]"
                    },
                    {
                        "components": [
                            {
                                "internalType": "address",
                                "name": "token",
                                "type": "address"
                            },
                            {
                                "internalType": "bool",
                                "name": "isActive",
                                "type": "bool"
                            },
                            {
                                "internalType": "uint256",
                                "name": "rate",
                                "type": "uint256"
                            }
                        ],
                        "internalType": "struct Launchbase.PoolCurrency[]",
                        "name": "supportedCurrencies",
                        "type": "tuple[]"
                    }
                ],
                "internalType": "struct Launchbase.PoolInfo[]",
                "name": "",
                "type": "tuple[]"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "uint256",
                "name": "_poolId",
                "type": "uint256"
            }
        ],
        "name": "getContributors",
        "outputs": [
            {
                "components": [
                    {
                        "internalType": "uint256",
                        "name": "poolId",
                        "type": "uint256"
                    },
                    {
                        "internalType": "address",
                        "name": "contributor",
                        "type": "address"
                    },
                    {
                        "internalType": "uint256",
                        "name": "amount",
                        "type": "uint256"
                    },
                    {
                        "internalType": "uint256",
                        "name": "timestamp",
                        "type": "uint256"
                    },
                    {
                        "internalType": "address",
                        "name": "currencyUsed",
                        "type": "address"
                    }
                ],
                "internalType": "struct Launchbase.ContributorInfo[]",
                "name": "",
                "type": "tuple[]"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "token",
                "type": "address"
            }
        ],
        "name": "getDecimal",
        "outputs": [
            {
                "internalType": "uint8",
                "name": "",
                "type": "uint8"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "owner",
        "outputs": [
            {
                "internalType": "address",
                "name": "",
                "type": "address"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "uint256",
                "name": "",
                "type": "uint256"
            }
        ],
        "name": "pools",
        "outputs": [
            {
                "internalType": "uint256",
                "name": "id",
                "type": "uint256"
            },
            {
                "internalType": "address",
                "name": "token",
                "type": "address"
            },
            {
                "internalType": "address",
                "name": "adminWallet",
                "type": "address"
            },
            {
                "internalType": "uint256",
                "name": "startTime",
                "type": "uint256"
            },
            {
                "internalType": "uint256",
                "name": "endTime",
                "type": "uint256"
            },
            {
                "internalType": "uint256",
                "name": "totalRaised",
                "type": "uint256"
            },
            {
                "internalType": "uint256",
                "name": "softCap",
                "type": "uint256"
            },
            {
                "internalType": "uint256",
                "name": "hardCap",
                "type": "uint256"
            },
            {
                "internalType": "enum Launchbase.PoolState",
                "name": "poolState",
                "type": "uint8"
            },
            {
                "internalType": "uint256",
                "name": "minContribution",
                "type": "uint256"
            },
            {
                "internalType": "uint256",
                "name": "maxContribution",
                "type": "uint256"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "proxiableUUID",
        "outputs": [
            {
                "internalType": "bytes32",
                "name": "",
                "type": "bytes32"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "totalPool",
        "outputs": [
            {
                "internalType": "uint256",
                "name": "",
                "type": "uint256"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "UPGRADE_INTERFACE_VERSION",
        "outputs": [
            {
                "internalType": "string",
                "name": "",
                "type": "string"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    }
];
const ERC_20ABI = [
    {
        "inputs": [],
        "stateMutability": "nonpayable",
        "type": "constructor"
    },
    {
        "anonymous": false,
        "inputs": [
            {
                "indexed": true,
                "internalType": "address",
                "name": "owner",
                "type": "address"
            },
            {
                "indexed": true,
                "internalType": "address",
                "name": "spender",
                "type": "address"
            },
            {
                "indexed": false,
                "internalType": "uint256",
                "name": "value",
                "type": "uint256"
            }
        ],
        "name": "Approval",
        "type": "event"
    },
    {
        "anonymous": false,
        "inputs": [
            {
                "indexed": true,
                "internalType": "address",
                "name": "account",
                "type": "address"
            },
            {
                "indexed": false,
                "internalType": "bool",
                "name": "isExcluded",
                "type": "bool"
            }
        ],
        "name": "ExcludeFromFees",
        "type": "event"
    },
    {
        "anonymous": false,
        "inputs": [
            {
                "indexed": true,
                "internalType": "address",
                "name": "previousOwner",
                "type": "address"
            },
            {
                "indexed": true,
                "internalType": "address",
                "name": "newOwner",
                "type": "address"
            }
        ],
        "name": "OwnershipTransferred",
        "type": "event"
    },
    {
        "anonymous": false,
        "inputs": [
            {
                "indexed": true,
                "internalType": "address",
                "name": "pair",
                "type": "address"
            },
            {
                "indexed": true,
                "internalType": "bool",
                "name": "value",
                "type": "bool"
            }
        ],
        "name": "SetAutomatedMarketMakerPair",
        "type": "event"
    },
    {
        "anonymous": false,
        "inputs": [
            {
                "indexed": true,
                "internalType": "address",
                "name": "from",
                "type": "address"
            },
            {
                "indexed": true,
                "internalType": "address",
                "name": "to",
                "type": "address"
            },
            {
                "indexed": false,
                "internalType": "uint256",
                "name": "value",
                "type": "uint256"
            }
        ],
        "name": "Transfer",
        "type": "event"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "owner",
                "type": "address"
            },
            {
                "internalType": "address",
                "name": "spender",
                "type": "address"
            }
        ],
        "name": "allowance",
        "outputs": [
            {
                "internalType": "uint256",
                "name": "",
                "type": "uint256"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "spender",
                "type": "address"
            },
            {
                "internalType": "uint256",
                "name": "amount",
                "type": "uint256"
            }
        ],
        "name": "approve",
        "outputs": [
            {
                "internalType": "bool",
                "name": "",
                "type": "bool"
            }
        ],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "",
                "type": "address"
            }
        ],
        "name": "automatedMarketMakerPairs",
        "outputs": [
            {
                "internalType": "bool",
                "name": "",
                "type": "bool"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "account",
                "type": "address"
            }
        ],
        "name": "balanceOf",
        "outputs": [
            {
                "internalType": "uint256",
                "name": "",
                "type": "uint256"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "bool",
                "name": "newStatus",
                "type": "bool"
            },
            {
                "internalType": "uint256",
                "name": "newInterval",
                "type": "uint256"
            }
        ],
        "name": "changeCooldownSettings",
        "outputs": [],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "cooldownEnabled",
        "outputs": [
            {
                "internalType": "bool",
                "name": "",
                "type": "bool"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "cooldownTimerInterval",
        "outputs": [
            {
                "internalType": "uint256",
                "name": "",
                "type": "uint256"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "decimals",
        "outputs": [
            {
                "internalType": "uint8",
                "name": "",
                "type": "uint8"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "spender",
                "type": "address"
            },
            {
                "internalType": "uint256",
                "name": "subtractedValue",
                "type": "uint256"
            }
        ],
        "name": "decreaseAllowance",
        "outputs": [
            {
                "internalType": "bool",
                "name": "",
                "type": "bool"
            }
        ],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "account",
                "type": "address"
            },
            {
                "internalType": "bool",
                "name": "excluded",
                "type": "bool"
            }
        ],
        "name": "excludeFromFees",
        "outputs": [],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "spender",
                "type": "address"
            },
            {
                "internalType": "uint256",
                "name": "addedValue",
                "type": "uint256"
            }
        ],
        "name": "increaseAllowance",
        "outputs": [
            {
                "internalType": "bool",
                "name": "",
                "type": "bool"
            }
        ],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "account",
                "type": "address"
            }
        ],
        "name": "isExcludedFromFees",
        "outputs": [
            {
                "internalType": "bool",
                "name": "",
                "type": "bool"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "marketingFee",
        "outputs": [
            {
                "internalType": "uint256",
                "name": "",
                "type": "uint256"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "marketingWallet",
        "outputs": [
            {
                "internalType": "address",
                "name": "",
                "type": "address"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "maxTxAmount",
        "outputs": [
            {
                "internalType": "uint256",
                "name": "",
                "type": "uint256"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "name",
        "outputs": [
            {
                "internalType": "string",
                "name": "",
                "type": "string"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "owner",
        "outputs": [
            {
                "internalType": "address",
                "name": "",
                "type": "address"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "renounceOwnership",
        "outputs": [],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "pair",
                "type": "address"
            },
            {
                "internalType": "bool",
                "name": "value",
                "type": "bool"
            }
        ],
        "name": "setAutomatedMarketMakerPair",
        "outputs": [],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "uint256",
                "name": "_marketingFee",
                "type": "uint256"
            }
        ],
        "name": "setFee",
        "outputs": [],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address payable",
                "name": "_newAddress",
                "type": "address"
            }
        ],
        "name": "setMarketingWallet",
        "outputs": [],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "uint256",
                "name": "newAmount",
                "type": "uint256"
            }
        ],
        "name": "setMaxTxAmount",
        "outputs": [],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "symbol",
        "outputs": [
            {
                "internalType": "string",
                "name": "",
                "type": "string"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "totalSupply",
        "outputs": [
            {
                "internalType": "uint256",
                "name": "",
                "type": "uint256"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "recipient",
                "type": "address"
            },
            {
                "internalType": "uint256",
                "name": "amount",
                "type": "uint256"
            }
        ],
        "name": "transfer",
        "outputs": [
            {
                "internalType": "bool",
                "name": "",
                "type": "bool"
            }
        ],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "sender",
                "type": "address"
            },
            {
                "internalType": "address",
                "name": "recipient",
                "type": "address"
            },
            {
                "internalType": "uint256",
                "name": "amount",
                "type": "uint256"
            }
        ],
        "name": "transferFrom",
        "outputs": [
            {
                "internalType": "bool",
                "name": "",
                "type": "bool"
            }
        ],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "newOwner",
                "type": "address"
            }
        ],
        "name": "transferOwnership",
        "outputs": [],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "uniswapV2Pair",
        "outputs": [
            {
                "internalType": "address",
                "name": "",
                "type": "address"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "uniswapV2Router",
        "outputs": [
            {
                "internalType": "contract IUniswapV2Router02",
                "name": "",
                "type": "address"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "stateMutability": "payable",
        "type": "receive"
    }
];
const ECET_ABI = [
    {
        "inputs": [
            {
                "internalType": "string",
                "name": "name",
                "type": "string"
            },
            {
                "internalType": "string",
                "name": "symbol",
                "type": "string"
            }
        ],
        "stateMutability": "nonpayable",
        "type": "constructor"
    },
    {
        "anonymous": false,
        "inputs": [
            {
                "indexed": true,
                "internalType": "address",
                "name": "owner",
                "type": "address"
            },
            {
                "indexed": true,
                "internalType": "address",
                "name": "spender",
                "type": "address"
            },
            {
                "indexed": false,
                "internalType": "uint256",
                "name": "value",
                "type": "uint256"
            }
        ],
        "name": "Approval",
        "type": "event"
    },
    {
        "anonymous": false,
        "inputs": [
            {
                "indexed": true,
                "internalType": "address",
                "name": "previousOwner",
                "type": "address"
            },
            {
                "indexed": true,
                "internalType": "address",
                "name": "newOwner",
                "type": "address"
            }
        ],
        "name": "OwnershipTransferred",
        "type": "event"
    },
    {
        "anonymous": false,
        "inputs": [
            {
                "indexed": true,
                "internalType": "address",
                "name": "from",
                "type": "address"
            },
            {
                "indexed": true,
                "internalType": "address",
                "name": "to",
                "type": "address"
            },
            {
                "indexed": false,
                "internalType": "uint256",
                "name": "value",
                "type": "uint256"
            }
        ],
        "name": "Transfer",
        "type": "event"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "owner",
                "type": "address"
            },
            {
                "internalType": "address",
                "name": "spender",
                "type": "address"
            }
        ],
        "name": "allowance",
        "outputs": [
            {
                "internalType": "uint256",
                "name": "",
                "type": "uint256"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "spender",
                "type": "address"
            },
            {
                "internalType": "uint256",
                "name": "amount",
                "type": "uint256"
            }
        ],
        "name": "approve",
        "outputs": [
            {
                "internalType": "bool",
                "name": "",
                "type": "bool"
            }
        ],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "account",
                "type": "address"
            }
        ],
        "name": "balanceOf",
        "outputs": [
            {
                "internalType": "uint256",
                "name": "",
                "type": "uint256"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "decimals",
        "outputs": [
            {
                "internalType": "uint8",
                "name": "",
                "type": "uint8"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "spender",
                "type": "address"
            },
            {
                "internalType": "uint256",
                "name": "subtractedValue",
                "type": "uint256"
            }
        ],
        "name": "decreaseAllowance",
        "outputs": [
            {
                "internalType": "bool",
                "name": "",
                "type": "bool"
            }
        ],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "spender",
                "type": "address"
            },
            {
                "internalType": "uint256",
                "name": "addedValue",
                "type": "uint256"
            }
        ],
        "name": "increaseAllowance",
        "outputs": [
            {
                "internalType": "bool",
                "name": "",
                "type": "bool"
            }
        ],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "name",
        "outputs": [
            {
                "internalType": "string",
                "name": "",
                "type": "string"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "owner",
        "outputs": [
            {
                "internalType": "address",
                "name": "",
                "type": "address"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "renounceOwnership",
        "outputs": [],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "uint256",
                "name": "_tax",
                "type": "uint256"
            }
        ],
        "name": "setTaxPercentage",
        "outputs": [],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "symbol",
        "outputs": [
            {
                "internalType": "string",
                "name": "",
                "type": "string"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "totalSupply",
        "outputs": [
            {
                "internalType": "uint256",
                "name": "",
                "type": "uint256"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "to",
                "type": "address"
            },
            {
                "internalType": "uint256",
                "name": "amount",
                "type": "uint256"
            }
        ],
        "name": "transfer",
        "outputs": [
            {
                "internalType": "bool",
                "name": "",
                "type": "bool"
            }
        ],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "from",
                "type": "address"
            },
            {
                "internalType": "address",
                "name": "to",
                "type": "address"
            },
            {
                "internalType": "uint256",
                "name": "amount",
                "type": "uint256"
            }
        ],
        "name": "transferFrom",
        "outputs": [
            {
                "internalType": "bool",
                "name": "",
                "type": "bool"
            }
        ],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "newOwner",
                "type": "address"
            }
        ],
        "name": "transferOwnership",
        "outputs": [],
        "stateMutability": "nonpayable",
        "type": "function"
    }
];

window.createPool = async function (contractAddress, account, _token, _adminWallet, _startTime, _endTime, _softCap, _hardCap, _minContribution, _maxContribution, _otherInfo, tokens, rates, isTokens) {
    try {
        if (window.ethereum != undefined) {
            window.web3 = new Web3(window.ethereum);
            var contract = new web3.eth.Contract(ABI, contractAddress);
            const apiKey = 'Y9SDE25YRER3NFFU6KGR25WZBNIS84IKNP';
            const averageGasPrice = await fetch(`https://api.polygonscan.com/api?module=gastracker&action=gasoracle&apikey=${apiKey}`)
                .then(response => response.json())
                .then(data => data.result.FastGasPrice);
            const gasPrice = web3.utils.toWei(averageGasPrice.toString(), 'gwei');
            if (contract !== null) {
                var gasEstimate = await contract.methods.createPool(_token, _adminWallet, _startTime, _endTime, _softCap, _hardCap, _minContribution, _maxContribution, _otherInfo, tokens, rates, isTokens).estimateGas({ from: account });
                const tx = {
                    from: account,
                    gas: gasEstimate,
                    gasPrice: gasPrice
                };
                var txHash = await contract.methods.createPool(_token, _adminWallet, _startTime, _endTime, _softCap, _hardCap, _minContribution, _maxContribution, _otherInfo, tokens, rates, isTokens).send(tx);
                var receipt = await web3.eth.getTransactionReceipt(txHash.transactionHash);
                while (receipt == null) {
                    await sleep(1000);
                    receipt = await web3.eth.getTransactionReceipt(txHash.transactionHash);
                }
                var result = `{
                                "status": ${receipt.status},
                                "transactionHash": "${txHash.transactionHash}"
                             }`;
                return result;
            }
        }
    } catch (e) {
        return e.message;
    }
}
window.contribute = async function (contractAddress, account, _token, _amount, isToken, poolId) {
    try
    {
        if (window.ethereum != undefined) {
            window.web3 = new Web3(window.ethereum);
            var contract = new web3.eth.Contract(ABI, contractAddress);
            const apiKey = 'Y9SDE25YRER3NFFU6KGR25WZBNIS84IKNP';
            const averageGasPrice = await fetch(`https://api.polygonscan.com/api?module=gastracker&action=gasoracle&apikey=${apiKey}`)
                .then(response => response.json())
                .then(data => data.result.FastGasPrice);
            const gasPrice = web3.utils.toWei(averageGasPrice.toString(), 'gwei');
            if (contract !== null) {
                if (isToken == false) {
                    var gasEstimate = await contract.methods.contribute(_token, _amount, isToken, poolId).estimateGas({ from: account, value: _amount });
                    const tx = {
                        from: account,
                        to: contractAddress,
                        gas: gasEstimate,
                        gasPrice: gasPrice,
                        value: _amount
                    };
                    var txHash = await contract.methods.contribute(_token, _amount, isToken, poolId).send(tx);
                    var receipt = await web3.eth.getTransactionReceipt(txHash.transactionHash);
                    while (receipt == null) {
                        await sleep(1000);
                        receipt = await web3.eth.getTransactionReceipt(txHash.transactionHash);
                    }
                    var result = `{
                                "status": ${receipt.status},
                                "transactionHash": "${txHash.transactionHash}"
                             }`;
                    return result;
                }
            }
        }
    } catch (e) {
        return e.message;
    }
}
window.getAllPools = async function (contractAddress) {
    try {
        if (window.ethereum != undefined) {
            window.web3 = new Web3(window.ethereum);
            var contract = new web3.eth.Contract(ABI, contractAddress);
            if (contract !== null) {
                var contract = new web3.eth.Contract(ABI, contractAddress);
                var data = await contract.methods.getAllPools().call();
                return data;
            }
        }
    } catch (e) {
        return e.message;
    }
}
window.getContributors = async function (contractAddress, poolId) {
    try {
        if (window.ethereum != undefined) {
            window.web3 = new Web3(window.ethereum);
            var contract = new web3.eth.Contract(ABI, contractAddress);
            if (contract !== null) {
                var contract = new web3.eth.Contract(ABI, contractAddress);
                var data = await contract.methods.getContributors(poolId).call();
                return data;
            }
        }
    } catch (e) {
        return e.message;
    }
}

window.approvePayment = async (contractAddress, address, tokenAddress, amount) => {
    try {
        if (window.ethereum != undefined) {
            window.web3 = new Web3(window.ethereum);
            var contract = new web3.eth.Contract(ERC_20ABI, contractAddress);
            if (contract !== null) {
                const apiKey = 'Y9SDE25YRER3NFFU6KGR25WZBNIS84IKNP';
                const averageGasPrice = await fetch(`https://api.polygonscan.com/api?module=gastracker&action=gasoracle&apikey=${apiKey}`)
                    .then(response => response.json())
                    .then(data => data.result.FastGasPrice);
                const gasPrice = web3.utils.toWei(averageGasPrice.toString(), 'gwei');
                var gasLimit = await contract.methods.approve(tokenAddress, amount).estimateGas({ from: address });
                var txHash = await contract.methods.approve(tokenAddress, amount).send({
                    from: address, gas: gasLimit, gasPrice: gasPrice
                }).on('transactionHash', function (hash) {
                    console.log('Transaction Hash:', hash);
                });
                var receipt = await web3.eth.getTransactionReceipt(txHash.transactionHash);
                while (receipt == null) {
                    await sleep(1000);
                    receipt = await web3.eth.getTransactionReceipt(txHash.transactionHash);
                }
                var result = `{
                                "status": ${receipt.status},
                                "transactionHash": "${txHash.transactionHash}"
                             }`;
                return result;
            }
            else {
                return "contract Error";
            }
        }
    } catch (e) {
        return e.message;
    }
}
window.GetAmountInWei = async (amount) => {
    try {
        var result = web3.utils.toWei(amount, "ether");
        return result;
    } catch (e) {

    }
    return null;

}
window.GetTokenDecimal = async (contractAddress) => {
    try {
        if (window.ethereum != undefined) {
            window.web3 = new Web3(window.ethereum);
            var contract = new web3.eth.Contract(ERC_20ABI, contractAddress);
            if (contract !== null) {
                var result = await contract.methods.decimals().call();
                return `${result.toString()}`;
            }
            else {
                return "contract Error";
            }
        }
    } catch (e) {
        return e.message;
    }
}
window.GetTokenName = async (contractAddress) => {
    try {
        if (window.ethereum != undefined) {
            window.web3 = new Web3(window.ethereum);
            var contract = new web3.eth.Contract(ERC_20ABI, contractAddress);
            if (contract !== null) {
                var result = await contract.methods.name().call();
                return `${result.toString()}`;
            }
            else {
                return "contract Error";
            }
        }
    } catch (e) {
        return e.message;
    }
}
window.GetTokenSymbol = async (contractAddress) => {
    try {
        if (window.ethereum != undefined) {
            window.web3 = new Web3(window.ethereum);
            var contract = new web3.eth.Contract(ERC_20ABI, contractAddress);
            if (contract !== null) {
                var result = await contract.methods.symbol().call();
                return `${result.toString()}`;
            }
            else {
                return "contract Error";
            }
        }
    } catch (e) {
        return e.message;
    }
}
window.GetTokenTotalSupply = async (contractAddress) => {
    try {
        if (window.ethereum != undefined) {
            window.web3 = new Web3(window.ethereum);
            var contract = new web3.eth.Contract(ERC_20ABI, contractAddress);
            if (contract !== null) {
                var result = await contract.methods.totalSupply().call();
                return `${result.toString()}`;
            }
            else {
                return "contract Error";
            }
        }
    } catch (e) {
        return e.message;
    }
}
window.GetTokenInformation = async (contractAddress) => {
    try {
        const tokenName = await GetTokenName(contractAddress);
        const tokenSymbol = await GetTokenSymbol(contractAddress);
        const tokenTotalSupply = await GetTokenTotalSupply(contractAddress);
        const tokenDecimal = await GetTokenDecimal(contractAddress);

        return '{' +
            '"Name":"' + tokenName + '",' +
            '"Symbol":"' + tokenSymbol + '",' +
            '"TotalSupply":' + tokenTotalSupply + ',' +
            '"Decimals":' + tokenDecimal + '' +
            '}';
    } catch (e) {
        return '{' +
            '"Name":null,' +
            '"Symbol":null,' +
            '"TotalSupply":null,' +
            '"Decimals":null,' +
            '"error":"' + e.message + '"' +
            '}';
    }
}
window.listenToChangeEvents = async function () {
    if (hasMetaMask()) {
        window.ethereum.on("connect", function (connectInfo) {
            DotNet.invokeMethodAsync('Launchbase.Services', 'OnConnect');
        });

        window.ethereum.on("disconnect", function (error) {
            DotNet.invokeMethodAsync('Launchbase.Services', 'OnDisconnect');
        });

        window.ethereum.on("accountsChanged", function (accounts) {
            DotNet.invokeMethodAsync('Launchbase.Services', 'OnAccountsChanged', accounts[0]);
        });

        window.ethereum.on("chainChanged", function (chainId) {
            DotNet.invokeMethodAsync('Launchbase.Services', 'OnChainChanged', chainId);
        });

    }
}
window.hasMetaMask = async function () {
    return (window.ethereum != undefined);
}
window.connectWallet = async function () {
    var result = await window.ethereum.request({
        method: 'eth_requestAccounts',
    });
    return result;
};
window.isConnected = async function () {
    return (window.ethereum != undefined && (ethereum.selectedAddress != undefined || ethereum.selectedAddress != null));
}
window.signAccount = async function (message) {
    const hex = message.split("")
        .map(c => c.charCodeAt(0).toString(16).padStart(2, "0"))
        .join("");
    const msg = `0x${hex}`;

    try {
        var result = await window.ethereum.request({
            method: 'personal_sign',
            params:
                [
                    msg,
                    window.ethereum.selectedAddress
                ]
        });

        return result;
    } catch (error) {
        // User denied account access...
        return "UserDenied"
    }
}
window.initializeWallet = async function () {
    if (window.ethereum) {
        try {
            await window.ethereum.request({ method: 'eth_requestAccounts' });
            // Continue with your application logic
        } catch (error) {
            console.error("Failed to initialize Ethereum:", error);
        }
    } else {
        console.error("Ethereum provider not found.");
    }
};